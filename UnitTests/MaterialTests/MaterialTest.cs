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
            Material mat = MaterialFactory.GetMaterial(MaterialType.Air);
            
            Assert.IsNotNull(mat);
        }
    }

   
}
