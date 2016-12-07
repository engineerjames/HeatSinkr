using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    [TestFixture]
    class CSVHeatsinkWriterTests
    {
        [Test]
        public void CSVFileWritesCorrectData()
        {
            string directory = @"C:\Test";
            IHeatsinkExporter writer = new CSVHeatsinkWriter();
            writer.Write(HeatsinkFactory.GetDefaultHeatsink(HeatsinkType.PlateFin), directory); 
        }
    }


}
