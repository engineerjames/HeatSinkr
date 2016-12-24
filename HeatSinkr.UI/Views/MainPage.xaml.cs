using HeatSinkr.Library;
using HeatSinkr.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HeatSinkr.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<string> Materials = new ObservableCollection<string>();
        private HeatSinkViewModel ViewModel { get; set; } = new HeatSinkViewModel();

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
            InitializeMaterialComboBox();
            CopyFilesIntoAppData();
            webView.Source = new Uri("ms-appdata:///local/WebAssets/index.html");
            ApplicationView.PreferredLaunchViewSize = new Windows.Foundation.Size(1050, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private async void CopyFilesIntoAppData()
        {
            try
            {
                var file1 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///WebViewAssets/baseWebView.js"));
                var file2 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///WebViewAssets/Chart.js"));
                var file3 = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///WebViewAssets/index.html"));
                var appData = await ApplicationData.Current.LocalFolder.GetFolderAsync("WebAssets");
                
                await file1.CopyAsync(appData, file1.DisplayName, NameCollisionOption.ReplaceExisting);
                await file2.CopyAsync(appData, file2.DisplayName, NameCollisionOption.ReplaceExisting);
                await file3.CopyAsync(appData, file3.DisplayName, NameCollisionOption.ReplaceExisting);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CopyFilesIntoAppData Error: " + ex.ToString());
            }
        }

        private void InitializeMaterialComboBox()
        {
            // Initialize list with all available materials
            foreach (var type in Enum.GetValues(typeof(MaterialType)))
            {
                Materials.Add(type.ToString());
            }

            MaterialComboBox.ItemsSource = Materials;

            // Set current heatsink material
            MaterialComboBox.SelectedItem = Enum.GetName(typeof(MaterialType), ViewModel.Material.Type);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var combo = sender as ComboBox;

            MaterialType mat;
            Enum.TryParse(combo.SelectedItem.ToString(), out mat);
            var materialToAssign = MaterialFactory.GetMaterial(mat);
            ViewModel.Material = materialToAssign;

            System.Diagnostics.Debug.WriteLine("New HS material: Total = " + ViewModel.ThermalResistance_Total);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void CommandBar_Closing(object sender, object e)
        {
            var commandBar = sender as CommandBar;
            commandBar.IsOpen = true;
        }

        private async void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            StorageFile file = await GetSaveDirectoryAsync();
            string dataToWrite = await ViewModel.WriteHeatsinkData(HeatsinkWriters.CSV);
            await Windows.Storage.FileIO.WriteTextAsync(file, dataToWrite);
        }

        private async Task<StorageFile> GetSaveDirectoryAsync()
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("CSV File", new List<string>() { ".csv" });
            savePicker.SuggestedFileName = "Heatsink " + DateTime.Now.ToString();

            StorageFile file = await savePicker.PickSaveFileAsync();         

            return file;
        }

        private async void NotImplementedClickHandler(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Feature not implemented yet!");
            dialog.Title = "Error!";
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            await dialog.ShowAsync();
        }

        private void Output_Value_Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                var html = HTMLEditor.Instance;
                var TrChartData = ViewModel.ThermalResistanceCurve;
                var PressureChartData = ViewModel.PressureCurve;
                html.UpdateHTML(TrChartData, PressureChartData);
                webView.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Output_Value_Changed Error: " + ex.ToString());
            }
        }

        private void Toggle_Checked(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleButton;

            if ((string)toggle.Content == "-")
            {
                toggle.Content = "+";
            }
            else
            {
                toggle.Content = "-";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.CFM = 5.001;
        }
    }
}
