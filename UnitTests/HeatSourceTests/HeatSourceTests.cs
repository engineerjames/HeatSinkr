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
            source.Length = 0.010;
            source.Width = 0.010;
        }

        [Test]
        public void CanBeConstructed()
        {
            HeatSource sourceTest = new HeatSource(7);
            Assert.NotNull(sourceTest);
        }
    }
}
