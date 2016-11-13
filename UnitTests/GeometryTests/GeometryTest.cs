using System;
using HeatSinkr.Library;
using NUnit.Framework;

namespace HeatSinkr.Tests
{
	[TestFixture]
	public class GeometryTest
	{
        public const double Epsilon = .000001;
        PlateFinGeometryParameters testParameters = new PlateFinGeometryParameters();
		Geometry<PlateFinGeometryParameters> testGeom;

		[SetUp]
		public void InitializeTestGeometry()
		{
			testParameters.FinThickness = .001;
			testParameters.FlowLength = .010;
			testParameters.Width = .040;
            testParameters.NumberOfFins = 11;
            testParameters.FinHeight = .035;
			testParameters.BaseThickness = .005;
			testGeom = new PlateFinGeometry(testParameters);
		}

		[Test]
		public void GeometryCanBeCreated()
		{
			Assert.AreNotEqual(null, testGeom);
		}

		[Test]
		public void GeometryParametersAreSetCorrectly()
		{
			PlateFinGeometryParameters actual = testGeom.GeometryDetails;
			Assert.AreEqual(actual, testParameters);

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
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.GeometryDetails.FlowLength = 0; });
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.GeometryDetails.FlowLength = -1; });
        }

        [Test]
        public void WidthOfZeroOrLessShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.GeometryDetails.Width = 0; });
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.GeometryDetails.Width = -1; });
        }

        [Test]
        public void NumberOfFinsZeroOrLessShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.GeometryDetails.NumberOfFins = 0; });
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.GeometryDetails.NumberOfFins = -1; });
        }

        [Test]
        public void NumberOfFinsLimitedByBaseWidth()
        {
            Assert.Throws<InvalidOperationException>(delegate { var DP = testGeom.GeometryDetails.NumberOfFins = 40; });
        }
    }
}
