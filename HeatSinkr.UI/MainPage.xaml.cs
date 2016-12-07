using HeatSinkr.Library;
using HeatSinkr.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
        }

        private void InitializeMaterialComboBox()
        {
            // Initialize list with all available materials
            foreach (var type in System.Enum.GetValues(typeof(MaterialType)))
            {
                Materials.Add(type.ToString());
            }
            MaterialComboBox.ItemsSource = Materials;

            // Set current heatsink material
            MaterialComboBox.SelectedItem = Enum.GetName(typeof(MaterialType), ViewModel.Material.Type);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Selection changed!");
            System.Diagnostics.Debug.WriteLine("Selected: " + MaterialComboBox.SelectedItem.ToString());
            var combo = sender as ComboBox;

            MaterialType mat;
            Enum.TryParse(combo.SelectedItem.ToString(), out mat);

            ViewModel.Material = MaterialFactory.GetMaterial(mat);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Implement me!");
        }

        private void CommandBar_Closing(object sender, object e)
        {
            var commandBar = sender as CommandBar;
            commandBar.IsOpen = true;
        }

      
    }
}
