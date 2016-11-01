using HeatSinkr.Library;
using NUnit.Framework;

namespace UnitTests.HeatSourceTests
{
    [TestFixture]
    public class HeatSourceTests
    {
        HeatSource source;

        [SetUp]
        public void InitializeSourceTests()
        {
            source = new HeatSource(7);
            source.Height = 0.025;
            source.Width = 0.025;
        }

        [Test]
        public void CanBeConstructed()
        {
            HeatSource sourceTest = new HeatSource(7);
            Assert.NotNull(sourceTest);
        }
    }
}
