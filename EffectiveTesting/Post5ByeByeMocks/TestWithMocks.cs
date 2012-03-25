using System;
using System.Collections.Generic;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace EffectiveTesting.Post5ByeByeMocks
{
    [TestFixture]
    public class TestWithMocks
    {
        [Test]
        public void Ensure_valid_prices_would_be_updated()
        {
            //Arrange.
            var currentDate = DateTime.Today;
            var price = new Price(10, currentDate.AddDays(-1));
            var pricesToUpdate = new List<Price>{ price };
        
            var repo = new MockRepository(MockBehavior.Loose);
        
            var transactionMock = repo.Create<ITransaction>();
            transactionMock
                .Setup(x => x.Commit())
                .Verifiable();

            var queryMock = repo.Create<IQuery>();
            queryMock
                .Setup(x => x.SetParameter("currentDate", currentDate))
                .Returns(queryMock.Object);
            queryMock
                .Setup(x => x.List<Price>())
                .Returns(pricesToUpdate);
        
            var sessionMock = repo.Create<ISession>();

            sessionMock
                .Setup(x => x.BeginTransaction())
                .Returns(transactionMock.Object)
                .Verifiable();
        
            sessionMock
                .Setup(x => x.CreateQuery("from Price p where p.ValidFrom <= :currentDate and :currentDate <= p.ValidTo"))
                .Returns(queryMock.Object);

            var sessionFactoryMock = repo.Create<ISessionManager>();
            sessionFactoryMock
                .Setup(x => x.OpenSession())
                .Returns(sessionMock.Object);
        
            var clockMock = repo.Create<IClock>();
            clockMock
                .SetupGet( x => x.Today)
                .Returns(currentDate);
        
            var rateProviderMock = repo.Create<IRateProvider>();
        
            rateProviderMock
                .Setup(x => x.GetRateOn(currentDate))
                .Returns(32.5m);

            var task = new UpdatePricesTask(sessionFactoryMock.Object, clockMock.Object, rateProviderMock.Object);
        
            //Act.
            task.Run();
        
            //Assert.
            Assert.That(price.LocalAmount, Is.EqualTo(325));
            repo.VerifyAll();
        }
    
        [Test]
        public void FooWorksFine()
        {
            var fooMock = new Mock<IFoo>();
            fooMock.SetupAllProperties();
            //other stuff
        }
    }
}