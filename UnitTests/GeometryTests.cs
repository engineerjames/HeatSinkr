using System;
using HeatSinkr.Library;
using NUnit.Framework;

namespace HeatSinkr.Tests
{
	[TestFixture]
	public class GeometryTests
	{
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
			PlateFinGeometryParameters actual = testGeom.GetGeometryParameters();
			Assert.AreEqual(actual, testParameters);

		}

		[Test]
		public void VolumeIsCalculatedCorrectly()
		{
			double ActualArea = testGeom.GetVolume();
			double ExpectedArea = (16000.0);
			Assert.AreEqual(ExpectedArea, ActualArea, Double.Epsilon);
		}

		[Test]
		public void FinGapIsCalculatedCorrectly()
		{
			double actual = testGeom.GetPitch();
			double expected = 2.90;

			Assert.AreEqual(expected, actual, Double.Epsilon);
		}
	}
}
