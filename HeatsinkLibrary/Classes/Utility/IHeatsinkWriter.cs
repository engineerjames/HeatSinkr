using System;

namespace HeatSinkr.Library
{
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
    /// Writes heatsink data to a .CSV file
    /// </summary>
    public class CSVHeatsinkWriter : IHeatsinkWriter
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
}
