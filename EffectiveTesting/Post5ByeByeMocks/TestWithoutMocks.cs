using EffectiveTesting.Post5ByeByeMocks;
using NHibernate;
using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;

public interface IFoo 
{
    bool IsFooed {get;set;}
}
[TestFixture]
public class TestWithoutMocks
{
    [Test]
    public void TestUsingMocks()
    {
        //Arrange.
        var currentDate = DateTime.Today;
        var price = new Price(10);
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
            .Setup(x => x.CreateQuery("from Price p where p.ValidFrom >= :currentDate and :currentDate <= p.ValidTo"))
            .Returns(queryMock.Object);

        var sessionFactoryMock = repo.Create<ISessionFactory>();
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
            .Returns(2);
        
        var task = new UpdatePricesTask(sessionFactoryMock.Object, Clock.FixedTime, rateProviderMock.Object);
        
        //Act.
        task.Run();
        
        //Assert.
        Assert.That(price.LocalAmount, Is.EqualTo(20));
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

