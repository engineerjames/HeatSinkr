using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HeatSinkr.Library;

namespace HeatSinkr.Tests
{
    [TestFixture]
    class CurveGeneratorTests
    {
        [Test]
        public void CanSetHeatsink()
        {
            var hsCurveGen = HeatsinkCurveGenerator.Instance;
        }
    }
}
