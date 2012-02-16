using System;
using NUnit.Framework;

namespace EffectiveTesting.Post2Instruments
{
    [TestFixture]
    public class VersionFixture
    {
        [Test]
        public void Constructor_parses_string_properly()
        {
            var version = Version.ParseWithError("1.2.330.400");

            //Обратите внимание, все сообщения консоли будут выведены в GUI NUnit
            //Часто таким способом полезно "логгировать" прохождене теста
            Console.WriteLine("Major={0} Minor={1} Build={2} Revision = {3}", version.Major, version.Minor, version.Build, version.Revision);

            Assert.AreEqual(1, version.Major);
            Assert.AreEqual(2, version.Minor);
            Assert.AreEqual(330, version.Build);
            Assert.AreEqual(400, version.Revision);
        }    
    
        [Test]
        public void Constructor_parses_string_properly_with_fluent_API()
        {
            var version = Version.Parse("1.2.330.400");

            //Обратите внимание, все сообщения консоли будут выведены в GUI NUnit
            //Часто таким способом полезно "логгировать" прохождене теста
            Console.WriteLine("Major={0} Minor={1} Build={2} Revision = {3}", version.Major, version.Minor, version.Build, version.Revision);

            Assert.That(version.Major, Is.EqualTo(1));
            Assert.That(version.Minor, Is.EqualTo(2));
            Assert.That(version.Build, Is.EqualTo(330));
            Assert.That(version.Revision, Is.EqualTo(400));
        }

        [Test]
        public void Constructor_parses_string_properly_in_the_wrong_way()
        {
            var version = Version.Parse("1.2.330.400");

            //Обратите внимание, все сообщения консоли будут выведены в GUI NUnit
            //Часто таким способом полезно "логгировать" прохождене теста
            Console.WriteLine("Major={0} Minor={1} Build={2} Revision = {3}", version.Major, version.Minor, version.Build, version.Revision);
        
            //Так делать очень плохо, т.к.. в итоге сообщение об ошибке будет невыразительным
            Assert.IsTrue(version.Major == 1);
            Assert.IsTrue(version.Minor == 2);
            Assert.IsTrue(version.Build == 330);
            Assert.IsTrue(version.Revision == 401);
        }
    }
}