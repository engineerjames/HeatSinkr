using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HeatSinkr.Library;

namespace HeatSinkr.Tests
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

   
}
