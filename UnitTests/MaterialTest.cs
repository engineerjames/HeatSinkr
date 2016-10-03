using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HeatSinkr.Library;

namespace UnitTests
{

    [TestFixture]
    class MaterialTest
    {
        public const double Epsilon = .000001;
        public const double MidEpsilon = .0001;
        public const double RoughEpsilon = .1;

        [Test]
        public void CanMaterialBeConstructed()
        {
            Material mat = new Air(50);
            
            Assert.IsNotNull(mat);
        }
    }

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
