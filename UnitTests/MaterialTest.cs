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
        [Test]
        public void CanMaterialBeConstructed()
        {
            Material mat = new Air(50);
            
            Assert.IsNotNull(mat);
        }
    }
}
