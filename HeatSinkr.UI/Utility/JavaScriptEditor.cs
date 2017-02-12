using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace HeatSinkr.UI
{
    public class JSEditor
    {
        private List<string> JSText;
        private StorageFolder appData = ApplicationData.Current.LocalFolder;

        public static JSEditor Instance { get; } = new JSEditor();
        
        public async void UpdateJSFileAsync(string TrChartData, string PressureChartData)
        {
            try
            {
                await GetJavaScriptFile();
                
                // The base javascript file is written such that the first line is the Thermal resistance chart information
                // and the second line is the pressure chart data.
                JSText[0] = TrChartData;
                JSText[1] = PressureChartData;
                var newFile = await appData.CreateFileAsync("baseWebView.js", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteLinesAsync(newFile, JSText);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateHTML Error: " + ex.ToString());
            }
        }

        private async Task GetJavaScriptFile()
        {
            try
            {
                if (!appData.Path.Contains("WebAssets"))
                {
                    appData = await appData.GetFolderAsync("WebAssets");
                }
                
                StorageFile javaScriptFile = await appData.GetFileAsync("baseWebView.js");
                var fullText = await FileIO.ReadLinesAsync(javaScriptFile);
                JSText = fullText.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetJavaScriptFile Error: " + ex.ToString());
            }
        }
    }
}
