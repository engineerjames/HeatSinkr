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
        public const double RoughEpsilon = .01;

        [Test]
        public void CanMaterialBeConstructed()
        {
            Material mat = new Air(50);
            
            Assert.IsNotNull(mat);
        }

        [Test]
        public void IsAirDensityAccurate()
        {
            Material mat = new Air(26.85);
            double actual = mat.Density;
            double expected = 1.177;

            Assert.AreEqual(expected, actual, RoughEpsilon);            
        }

        [Test]
        public void IsThermalConductivityAccurate()
        {
            Material mat = new Air(26.85);
            double actual = mat.ThermalConductivity;
            double expected = .02624;

            Assert.AreEqual(expected, actual, MidEpsilon);
        }

        [Test]
        public void IsDynamicViscosityAccurate()
        {
            Material mat = new Air(26.85);
            double actual = mat.DynamicViscosity;
            double expected = 0.00001846;

            Assert.AreEqual(expected, actual, Epsilon);
        }
    }
}
