using System;
using HeatSinkr.Library;
using NUnit.Framework;

namespace HeatSinkr.Tests
{
	[TestFixture]
	public class GeometryTest
	{
        public const double Epsilon = .000001;
        PlateFinGeometry testGeom = new PlateFinGeometry();

		[SetUp]
		public void InitializeTestGeometry()
		{
            testGeom.FinThickness = .001;
            testGeom.FlowLength = .010;
            testGeom.Width = .040;
            testGeom.NumberOfFins = 11;
            testGeom.FinHeight = .035;
            testGeom.BaseThickness = .005;
		}

		[Test]
		public void GeometryCanBeCreated()
		{
			Assert.AreNotEqual(null, testGeom);
		}

		[Test]
		public void VolumeIsCalculatedCorrectly()
		{
			double actualVolume = testGeom.Volume;
            double expectedVolume = 5850.0 * System.Math.Pow(10, -9); 
			Assert.AreEqual(expectedVolume, actualVolume, Epsilon);
		}

		[Test]
		public void FinGapIsCalculatedCorrectly()
		{
			double actual = testGeom.Pitch;
			double expected = .0029; 

			Assert.AreEqual(expected, actual, Epsilon);
		}

        [Test]
        public void SurfaceAreaIsCalculatedCorrectly()
        {
            double actual = testGeom.SurfaceArea;
            double expected = 9770*System.Math.Pow(10,-6); 

            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void HydraulicDiameterIsCalculatedCorrectly()
        {
            double actual = testGeom.CharacteristicLength;
            double expected = .00535620052770449;

            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void AspectRatioCalculatedCorrectly()
        {
            double actual = testGeom.AspectRatio;
            double expected = 0.0828571428571429000;

            Assert.AreEqual(expected, actual, Epsilon);
        }

        

        [Test]
        public void ZeroOrLessFlowLengthShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.FlowLength = 0; });
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.FlowLength = -1; });
        }

        [Test]
        public void WidthOfZeroOrLessShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.Width = 0; });
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.Width = -1; });
        }

        [Test]
        public void NumberOfFinsZeroOrLessShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.NumberOfFins = 0; });
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.NumberOfFins = -1; });
        }

        [Test]
        public void NumberOfFinsLimitedByBaseWidth()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.NumberOfFins = 40; });
        }

        [Test]
        public void FinHeightOfZeroOrLessShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.FinHeight = 0; });
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.FinHeight = -1; });
        }

        [Test]
        public void FinThicknessOfZeroOrLessShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.FinThickness = 0; });
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.FinThickness = -1; });
        }

        [Test]
        public void BaseHeightOfZeroOrLessShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.BaseThickness = 0; });
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.BaseThickness = -1; });
        }
    }
}
