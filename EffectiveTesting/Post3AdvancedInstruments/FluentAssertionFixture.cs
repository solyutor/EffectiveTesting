using FluentAssertions;
using NUnit.Framework;

namespace EffectiveTesting.Post3AdvancedInstruments
{
    [TestFixture]
    public class FluentAssertionFixture
    {
        [Test]
        public void FluentAssertions_helps_to_improve_multi_assertion()
        {
            var version = Version.ParseWithError("1.2.3.4");

            version.ShouldHave().AllProperties().EqualTo(new Version(1, 2, 3, 4));
        }

        [Test]
        public void FluentAssertions_with_anonymous()
        {
            var version = Version.ParseWithError("1.2.3.4");

            version.Should().Match<Version>(x => x.Major == 1 && x.Minor == 3);

            version.ShouldHave().AllProperties().EqualTo(new {Major = 1, Minor = 2, Build = 3, Revision = 4});
        }

        [Test]
        public void FluentAssertions_test_shared_fields()
        {
            var version = Version.Parse("1.2.3.4");

            var shortVersion = version.ToShortVersion();

            shortVersion.ShouldHave().SharedProperties().EqualTo(version);
        }
    }
}