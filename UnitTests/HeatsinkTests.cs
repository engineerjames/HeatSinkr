using System;
using NUnit.Framework;
using HeatSinkr.Library;

namespace HeatSinkr.Tests
{
	[TestFixture]
	public class HeatsinkTests
	{
		[Test]
        public void CanBeConstructed()
        {
            PlateFinGeometryParameters testParameters = new PlateFinGeometryParameters();
            PlateFinGeometry testGeom;
            testParameters.NumberOfFins = 11;
            testParameters.FinThickness = 1.0;
            testParameters.FlowLength = 10.0;
            testParameters.Width = 40.0;
            testParameters.FinHeight = 35.0;
            testParameters.BaseThickness = 5.0;
            testGeom = new PlateFinGeometry(testParameters);
            
            PlateFinHeatsink hs = new PlateFinHeatsink(new Air(30), testGeom);
            
            Assert.IsNotNull(hs);
        }
    }



}

