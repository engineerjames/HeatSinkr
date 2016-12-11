using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace HeatSinkr.UI
{
    public class HTMLEditor
    {
        private static int i = 0;
        private StorageFile javaScriptFile;
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
                System.Diagnostics.Debug.WriteLine(ex.ToString());
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

                this.javaScriptFile = await appData.GetFileAsync("baseWebView.js");
                System.Diagnostics.Debug.WriteLine("Javascript file: " + javaScriptFile?.DisplayName + " at " + javaScriptFile.Path);
                var fullText = await FileIO.ReadLinesAsync(javaScriptFile);
                JSText = fullText.ToList();
                foreach (string line in JSText)
                {
                    System.Diagnostics.Debug.WriteLine("FULL TEXT: " + line);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetJavaScriptFile Error: " + ex.ToString());
            }
        }
    }
}
