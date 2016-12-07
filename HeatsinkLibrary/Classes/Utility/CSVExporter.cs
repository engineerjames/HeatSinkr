using System;
using System.Reflection;

namespace HeatSinkr.Library
{

    /// <summary>
    /// Writes heatsink data to a .CSV file
    /// </summary>
    public class CSVExporter : IHeatsinkExporter
    {
        string IHeatsinkExporter.GetWriteableData(Heatsink hs)
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

            return textToWrite;
        }
    }
}
