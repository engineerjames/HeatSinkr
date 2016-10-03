using NUnit.Framework;
using HeatSinkr.Library;


namespace HeatSinkr.Tests
{

    [TestFixture]
    class AirTest
    {
        public const double Epsilon = .000001;
        public const double MidEpsilon = .0001;
        public const double RoughEpsilon = .1;

        Material mat;

        [SetUp]
        public void SetUp()
        {
            mat = new Air(26.85);
        }

        [Test]
        public void IsAirDensityAccurate()
        {
            double actual = mat.Density;
            double expected = 1.177;

            Assert.AreEqual(expected, actual, RoughEpsilon);
        }

        [Test]
        public void IsThermalConductivityAccurate()
        {
            double actual = mat.ThermalConductivity;
            double expected = .02624;

            Assert.AreEqual(expected, actual, MidEpsilon);
        }

        [Test]
        public void IsDynamicViscosityAccurate()
        {
            double actual = mat.DynamicViscosity;
            double expected = 0.00001846;

            Assert.AreEqual(expected, actual, Epsilon);
        }

        [Test]
        public void IsSpecificHeatAccurate_CP()
        {
            double actual = mat.SpecificHeat;
            double expected = 1004.9;

            Assert.AreEqual(expected, actual, RoughEpsilon);
        }

        [Test]
        public void IsPrandtlNumberAccurate()
        {
            double actual = mat.Prandtl;
            double expected = 0.707;

            Assert.AreEqual(expected, actual, MidEpsilon);
        }

        [Test]
        public void IsThermalDiffusivityAccurate()
        {
            double actual = mat.Diffusivity;
            double expected = 22.07 * 0.00001;

            Assert.AreEqual(expected, actual, Epsilon);
        }

    }

}
