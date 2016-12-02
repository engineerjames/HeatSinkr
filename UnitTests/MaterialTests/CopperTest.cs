using HeatSinkr.Library;
using NUnit.Framework;

namespace HeatSinkr.Tests
{
    [TestFixture]
    class CopperTest
    {
        public const double Epsilon = .000001;
        public const double MidEpsilon = .0001;
        public const double RoughEpsilon = .1;

        Material mat;

        [SetUp]
        public void Setup()
        {
            mat = new Copper();
        }

        [Test]
        public void IsThermalConductivityAccurate()
        {
            double actual = mat.ThermalConductivity;
            double expected = 400; // W/m-K
            
            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void IsDensityAccurate()
        {
            double actual = mat.Density;
            double expected = 8940; // kg/m^3

            Assert.AreEqual(expected, actual, Epsilon);
        }
    }
}
