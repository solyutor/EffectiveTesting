using System;
using EffectiveTesting.Post5ByeByeMocks;
using NHibernate;

public class UpdatePricesTask : ITask
{
    private ISessionManager _sessionManager;
    private IClock _clock;
    private IRateProvider _rateProvider;
    
    public UpdatePricesTask(ISessionManager sessionManager, IClock clock, IRateProvider rateProvider)
    {
        _sessionManager = sessionManager;
        _clock = clock;
        _rateProvider = rateProvider;
    }
    
    public void Run()
    {
        var rate = _rateProvider.GetRateOn(_clock.Today);
        
        using(var session = _sessionManager.OpenSession())
        using(var tx = session.BeginTransaction())
        {
            var prices = session
                .CreateQuery("from Price p where p.ValidFrom <= :currentDate and :currentDate <= p.ValidTo")
                .SetParameter("currentDate", _clock.Today)
                .List<Price>();

            foreach(var price in prices)
            {
                price.UpdateLocalPriceUsing(rate);
                price.LastUpdated = _clock.Today;
            }
            session.Flush();
            tx.Commit();
        }
    }
}