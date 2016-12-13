using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace HeatSinkr.UI
{
    public class HTMLEditor
    {
        private static int i = 0;
        private List<string> JSText;
        private StorageFolder appData = ApplicationData.Current.LocalFolder;

        public static HTMLEditor Instance { get; } = new HTMLEditor();
        
        public async void UpdateHTML(string chartData)
        {
            try
            {
                await GetJavaScriptFile();
                JSText[0] = chartData;
                var newFile = await appData.CreateFileAsync("baseWebView.js", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteLinesAsync(newFile, JSText);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateHTML Error: " + ex.ToString());
            }
        }

        private HTMLEditor()
        {
        }

        private async Task GetJavaScriptFile()
        {
            try
            {
                appData = await appData.GetFolderAsync("WebAssets");

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
