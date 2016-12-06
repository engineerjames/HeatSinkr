using System;
using System.Reflection;

namespace HeatSinkr.Library
{

    /// <summary>
    /// Writes heatsink data to a .CSV file
    /// </summary>
    public class CSVHeatsinkWriter : IHeatsinkWriter
    {
        void IHeatsinkWriter.Write(Heatsink hs, string directory)
        {
            string textToWrite = "";
            var properties = typeof(Heatsink).GetRuntimeProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType != Type.GetType("HeatSinkr.Library.Geometry") || property.PropertyType != Type.GetType("HeatSinkr.Library.Material"))
                {
                    textToWrite += property.Name + ", " + property.GetValue(hs) + Environment.NewLine;
                }
                else
                {
                    textToWrite += property.Name + ", " + property.GetValue(hs) + Environment.NewLine;
                }
            }

            System.IO.File.WriteAllText(directory, textToWrite);
        }
    }
}
