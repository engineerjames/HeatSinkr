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
			testParameters.NumberOfFins = 11;
			testParameters.FinThickness = 1.0;
			testParameters.FlowLength = 10.0;
			testParameters.Width = 40.0;
			testParameters.FinHeight = 35.0;
			testParameters.BaseThickness = 5.0;
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
			double actualVolume = testGeom.GetVolume();
			double expectedVolume = 5850.0; // mm^3
			Assert.AreEqual(expectedVolume, actualVolume, Epsilon);
		}

		[Test]
		public void FinGapIsCalculatedCorrectly()
		{
			double actual = testGeom.GetPitch();
			double expected = 2.90; // mm^3

			Assert.AreEqual(expected, actual, Epsilon);
		}

        [Test]
        public void SurfaceAreaIsCalculatedCorrectly()
        {
            double actual = testGeom.GetSurfaceArea();
            double expected = 9770; // mm^2

            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void HydraulicDiameterIsCalculatedCorrectly()
        {
            double actual = testGeom.GetCharacteristicLength();
            double expected = 5.35620052770449;

            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void AspectRatioCalculatedCorrectly()
        {
            double actual = testGeom.AspectRatio();
            double expected = 0.0828571428571429000;

            Assert.AreEqual(expected, actual, Epsilon);
        }
	}
}
