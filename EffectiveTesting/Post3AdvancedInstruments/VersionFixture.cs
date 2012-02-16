using NUnit.Framework;
using SharpTestsEx;

namespace EffectiveTesting.Post3AdvancedInstruments
{
    [TestFixture]
    public class VersionFixture
    {
        [Test]
        public void Is_not_good_testing_because_some_asserts_are_missed()
        {
            var version = Version.ParseWithError("1.2.3.4");

            Assert.AreEqual(1, version.Major);
            Assert.AreEqual(2, version.Minor);
            Assert.AreEqual(3, version.Build);
            Assert.AreEqual(4, version.Revision);
        }

        [Test]
        public void SharpTestsEx_helps_to_improve_multi_assertion()
        {
            var version = Version.ParseWithError("1.2.3.4");

            version.Satisfy(x =>
                            x.Major == 1 &&
                            x.Minor == 2 &&
                            x.Build == 3 &&
                            x.Revision == 4);
        }
    }
}