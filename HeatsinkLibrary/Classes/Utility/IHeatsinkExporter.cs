using System;

namespace HeatSinkr.Library
{
    public enum HeatsinkWriters { HTML, PDML, CSV };

    public interface IHeatsinkExporter
    {
        string GetWriteableData(Heatsink hs);
    }

    /// <summary>
    /// Writes heatsink data to an HTML file - with plots using Chart.js
    /// </summary>
    public class HTMLExporter : IHeatsinkExporter
    {
        string IHeatsinkExporter.GetWriteableData(Heatsink hs)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Writes heatsink geometry only to a PDML file for easy import into FloTHERM.
    /// </summary>
    public class PDMLExporter : IHeatsinkExporter
    {
        string IHeatsinkExporter.GetWriteableData(Heatsink hs)
        {
            throw new NotImplementedException();
        }
    }

    public static class HeatsinkWriterFactory
    {
        public static IHeatsinkExporter GetWriter(HeatsinkWriters type)
        {
            switch (type)
            {
                case HeatsinkWriters.HTML:
                    return new HTMLExporter();
                case HeatsinkWriters.PDML:
                    return new PDMLExporter();
                case HeatsinkWriters.CSV:
                    return new CSVExporter();
                default:
                    throw new InvalidOperationException("Not a known type!");
            }
        }
    }
}
