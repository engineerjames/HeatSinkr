using System;

namespace HeatSinkr.Library
{
    public enum HeatsinkWriters { HTML, PDML, CSV };

    public interface IHeatsinkWriter
    {
        void Write(Heatsink hs, string directory);
    }

    /// <summary>
    /// Writes heatsink data to an HTML file - with plots using Chart.js
    /// </summary>
    public class HTMLHeatsinkWriter : IHeatsinkWriter
    {
        void IHeatsinkWriter.Write(Heatsink hs, string directory)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Writes heatsink geometry only to a PDML file for easy import into FloTHERM.
    /// </summary>
    public class PDMLHeatsinkWriter : IHeatsinkWriter
    {
        void IHeatsinkWriter.Write(Heatsink hs, string directory)
        {
            throw new NotImplementedException();
        }
    }

    public static class HeatsinkWriterFactory
    {
        public static IHeatsinkWriter GetWriter(HeatsinkWriters type)
        {
            switch (type)
            {
                case HeatsinkWriters.HTML:
                    return new HTMLHeatsinkWriter();
                case HeatsinkWriters.PDML:
                    return new PDMLHeatsinkWriter();
                case HeatsinkWriters.CSV:
                    return new CSVHeatsinkWriter();
                default:
                    throw new InvalidOperationException("Not a known type!");
            }
        }
    }
}
