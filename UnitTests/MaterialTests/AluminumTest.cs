using HeatSinkr.Library;
using NUnit.Framework;

namespace HeatSinkr.Tests
{
    [TestFixture]
    class AluminumTest
    {
        public const double Epsilon = .000001;
        public const double MidEpsilon = .0001;
        public const double RoughEpsilon = .1;

        Material mat;

        [SetUp]
        public void Setup()
        {
            mat = new Aluminum();
        }

        [Test]
        public void IsThermalConductivityAccurate()
        {
            double actual = mat.ThermalConductivity;
            double expected = 204; // W/m-K

            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void IsDensityAccurate()
        {
            double actual = mat.Density;
            double expected = 2700; // kg/m^3

            Assert.AreEqual(expected, actual, Epsilon);
        }
    }
}
