using Enhima;
using NHibernate.Cfg;
using NUnit.Framework;
using SharpTestsEx;

namespace EffectiveTesting.Post5ByeByeMocks
{
    [TestFixture]
    public class TestWithoutMocks
    {
        private Configuration _configuration;
        private SQLiteInMemoryTestHelper _testHelper;
        private EnhimaSessionManager _sessionManager;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _configuration = new Configuration();//.DataBaseIntegration(db => db.LogSqlInConsole = true);

            _configuration.ConfigureSQLiteInMemory();
            _configuration.MapEntities(From.ThisApplication());
            _testHelper = new SQLiteInMemoryTestHelper(_configuration);
            

            _sessionManager = new EnhimaSessionManager(_testHelper);
        }

        [SetUp]
        public void SetUp()
        {
            _testHelper.CreateSchema();
        }

        [TearDown]
        public void TearDown()
        {
            _testHelper.DropSchema();
        }

        [Test]
        public void Ensure_valid_prices_would_be_updated()
        {
            var today = Clock.FixedTime.Today;
            var yesterday = today.AddDays(-1);

            var priceToUpdate = new Price(10, yesterday) { ValidFrom = yesterday, ValidTo = today };
            var priceToSkip = new Price(20, yesterday) { ValidFrom = yesterday, ValidTo = yesterday };

            _testHelper.Persist(priceToUpdate, priceToSkip);

            var rateProviderStub = new RateProviderStub();

            var task = new UpdatePricesTask(_sessionManager, Clock.FixedTime, rateProviderStub);

            task.Run();

            var savedPriceToUpdate = _testHelper.Load<Price>(priceToUpdate.Id);
            var savedPriceToSkip = _testHelper.Load<Price>(priceToSkip.Id);

            savedPriceToUpdate.Satisfy(x =>
                                       x.LocalAmount == 325m &&
                                       x.LastUpdated == Clock.FixedTime.Today);
            
            savedPriceToSkip.Satisfy(x =>
                                     x.LocalAmount == 0 &&
                                     x.LastUpdated == yesterday);
        }        
        
        [Test]
        public void Ensure_valid_prices_would_be_updated2()
        {
            var today = Clock.FixedTime.Today;
            var yesterday = today.AddDays(-1);

            var priceToUpdate = new Price(10, yesterday) { ValidFrom = yesterday, ValidTo = today };
            var priceToSkip = new Price(20, yesterday) { ValidFrom = yesterday, ValidTo = yesterday };

            _testHelper.Persist(priceToUpdate, priceToSkip);

            var rateProviderStub = new RateProviderStub();

            var task = new UpdatePricesTask(_sessionManager, Clock.FixedTime, rateProviderStub);

            task.Run();

            var savedPriceToUpdate = _testHelper.Load<Price>(priceToUpdate.Id);
            var savedPriceToSkip = _testHelper.Load<Price>(priceToSkip.Id);

            savedPriceToUpdate.Satisfy(x =>
                                       x.LocalAmount == 325m &&
                                       x.LastUpdated == Clock.FixedTime.Today);
            
            savedPriceToSkip.Satisfy(x =>
                                     x.LocalAmount == 0 &&
                                     x.LastUpdated == yesterday);
        }
    }
}