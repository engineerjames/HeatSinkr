using System;
using NUnit.Framework;

namespace HeatsinkLibrary
{
	[TestFixture]
	public class GeometryTests
	{
		GeometryParameters testParameters = new GeometryParameters();
		Geometry testGeom;


		public void InitializeTestGeometry()
		{
			testParameters.FinThickness = 1.0;
			testParameters.FlowLength = 10.0;
			testParameters.Width = 40.0;
			testParameters.FinHeight = 35.0;
			testParameters.BaseThickness = 5.0;
			testGeom = new Geometry(testParameters);
		}

		[Test]
		public void GeometryCanBeCreated()
		{
			InitializeTestGeometry();
			Assert.AreNotEqual(null, testGeom);
		}

		[Test]
		public void GeometryParametersAreSetCorrectly()
		{
			GeometryParameters actual = testGeom.GetGeometryParameters();
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
		public void CannotSetHeatsinkHeight()
		{
			Assert.Throws(InvalidOperationException, testGeom.GetGeometryParameters().Height = 5.0);
		}

		[Test]
		public void FinGapIsCalculatedCorrectly()
		{
			
		}

	}
}
