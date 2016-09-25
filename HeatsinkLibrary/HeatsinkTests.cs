using System;
using NUnit.Framework;

namespace HeatsinkLibrary
{
	[TestFixture]
	public class HeatsinkTests
	{
		
	}

	[TestFixture]
	public class UnitsTests
	{
		[Test]
		public void ConvertsFromMMtoM()
		{
			double valuetoConvert = 100.0;
			double returnValue = Units.ConvertMMtoM(valuetoConvert);
			double expectedValue = 0.1;

			Assert.AreEqual(expectedValue, returnValue, Double.Epsilon);
		}

		[Test]
		public void ConvertsFromMtoMM()
		{
			double valuetoConvert = 100.0;
			double returnValue = Units.ConvertMtoMM(valuetoConvert);
			double expectedValue = 100000;

			Assert.AreEqual(returnValue, expectedValue, Double.Epsilon);
		}
	}

}

