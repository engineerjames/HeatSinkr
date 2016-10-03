﻿using System;
using HeatSinkr.Library;
using NUnit.Framework;


namespace HeatSinkr.Tests
{

	[TestFixture]
	public class UnitsTests
	{
		public const double Epsilon = .000001;

		[Test]
		public void ConvertsFromMMtoM()
		{
			double valuetoConvert = 100.0;
			double returnValue = Units.ConvertMMtoM(valuetoConvert);
			double expectedValue = 0.1;

			Assert.AreEqual(expectedValue, returnValue, Epsilon);
		}

		[Test]
		public void ConvertsFromMtoMM()
		{
			double valuetoConvert = 100.0;
			double returnValue = Units.ConvertMtoMM(valuetoConvert);
			double expectedValue = 100000;

			Assert.AreEqual(returnValue, expectedValue, Epsilon);
		}

		[Test]
		public void ConvertsFromH2OToPa()
		{
			double valuetoConvert = 1.324;
			double expectedValue = 329.46416;
			double actualValue = Units.ConvertFromH2OToPa(valuetoConvert);

			Assert.AreEqual(expectedValue, actualValue, Epsilon);
		}

		[Test]
		public void ConvertsFromPaToH2O()
		{
			double valuetoConvert = 1008.0;
			double expectedValue = 4.050796;
			double actualValue = Units.ConvertFromPaToH2O(valuetoConvert);

			Assert.AreEqual(expectedValue, actualValue, Epsilon);
		}

        [Test]
        public void ConvertsFromCFMToMetersCubedPerSecond()
        {
            double valueToConvert = 5; // CFM
            double expectedValue = 0.0023597372;
            double actualValue = Units.ConvertCFMToMetersCubedPerSecond(valueToConvert);

            Assert.AreEqual(expectedValue, actualValue, Epsilon);
        }

        [Test]
        public void ConvertsFromMetersCubedPerSecondToCFM()
        {
            double valueToConvert = 1; // m^3/s
            double expectedValue = 2118.880003;
            double actualValue = Units.ConvertFromMetersCubedPerSecondToCFM(valueToConvert);

            Assert.AreEqual(expectedValue, actualValue, Epsilon);
        }
	}
}

