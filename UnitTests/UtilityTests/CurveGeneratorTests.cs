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
        PlateFinHeatsink hs;
        HeatSource heat;
        public const double Epsilon = .000001;
        public const double RoughEpsilon = 0.01;

        [SetUp]
        public void SetupHSTests()
        {
            PlateFinGeometry testGeom = new PlateFinGeometry();
            testGeom.FinThickness = .001;
            testGeom.FlowLength = .010;
            testGeom.Width = .040;
            testGeom.FinHeight = .035;
            testGeom.BaseThickness = .005;
            testGeom.NumberOfFins = 11;

            hs = new PlateFinHeatsink(new Aluminum(), testGeom);
            hs.CFM = 5;

            heat = new HeatSource(4);
            heat.Length = 0.010;
            heat.Width = 0.010;
            hs.Source = heat;
        }

        [Test]
        public void CanGrabOnlyOneInstance()
        {
            var hsCurveGen = CurveGenerator.Instance;
            var hsCurveGen2 = CurveGenerator.Instance;

            Assert.AreSame(hsCurveGen, hsCurveGen2);
        }

        [Test]
        public void CannotAnalyzeZeroHeatsinks()
        {
            var hsCurveGen = CurveGenerator.Instance;
            Assert.Throws<InvalidOperationException>(delegate { hsCurveGen.GetThermalResistanceCurves(0.5, 15.0); });
        }

        [Test]
        public void CannotPassInInvalidCFM()
        {
            var hsCurveGen = CurveGenerator.Instance;
            hsCurveGen.AddHeatsink(hs);

            // Shouldn't be able to pass in 0 or less into the low CFM
            Assert.Throws<InvalidOperationException>(delegate { hsCurveGen.GetThermalResistanceCurves(-1.0, 1.0); });

            // Shouldn't be able to pass in 0 or less into the high CFM
            Assert.Throws<InvalidOperationException>(delegate { hsCurveGen.GetThermalResistanceCurves(1.0, -1.0); });
        }

        [Test]
        public void CFMCurveIsAccurate()
        {
            var hsCurveGen = CurveGenerator.Instance;
            hsCurveGen.AddHeatsink(hs);

            var TrCurves = hsCurveGen.GetThermalResistanceCurves(0.5, 15);

            for (int i = 0; i < TrCurves.Count; i++)
            {
                if (i > 0)
                {
                    Assert.Less(TrCurves[0][i].Y, TrCurves[0][i - 1].Y);
                    Assert.Greater(TrCurves[0][i].X, TrCurves[0][i - 1].X);
                }
            }
        }
    }
}
