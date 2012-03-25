using Enhima;
using NHibernate.Cfg;
using NUnit.Framework;
using SharpTestsEx;

namespace EffectiveTesting.Post5ByeByeMocks
{
    [TestFixture]
    public class TestWithoutMocks
    {
        [Test]
        public void Ensure_valid_prices_would_be_updated()
        {
            var configuration = new Configuration().DataBaseIntegration(db => db.LogSqlInConsole = true );

            configuration.ConfigureSQLiteInMemory();
            configuration.MapEntities(From.ThisApplication());
            var testHelper = new SQLiteInMemoryTestHelper(configuration);
            testHelper.CreateSchema();

            var manager = new EnhimaSessionManager(testHelper);

            var today = Clock.FixedTime.Today;
            var yesterday = today.AddDays(-1);

            var priceToUpdate = new Price(10, yesterday) { ValidFrom = yesterday, ValidTo = today };
            var priceToSkip = new Price(20, yesterday) { ValidFrom = yesterday, ValidTo = yesterday };

            testHelper.Persist(priceToUpdate, priceToSkip);

            var rateProviderStub = new RateProviderStub();

            var task = new UpdatePricesTask(manager, Clock.FixedTime, rateProviderStub);

            task.Run();

            var savedPriceToUpdate = testHelper.Load<Price>(priceToUpdate.Id);
            var savedPriceToSkip = testHelper.Load<Price>(priceToSkip.Id);

            savedPriceToUpdate.Satisfy(x =>
                                       x.LocalAmount == 325m &&
                                       x.LastUpdated == Clock.FixedTime.Today);
            
            savedPriceToSkip.Satisfy(x =>
                                     x.LocalAmount == 0 &&
                                     x.LastUpdated == yesterday);
        }
    }
}