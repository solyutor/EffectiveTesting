using NHibernate;

public class UpdatePricesTask : ITask
{
    private ISessionFactory _sessionFactory;
    private IClock _clock;
    private IRateProvider _rateProvider;
    
    public UpdatePricesTask(ISessionFactory sessionFactory, IClock clock, IRateProvider rateProvider)
    {
        _sessionFactory = sessionFactory;
        _clock = clock;
        _rateProvider = rateProvider;
    }
    
    public void Run()
    {
        var rate = _rateProvider.GetRateOn(_clock.Today);
        
        using(var session = _sessionFactory.OpenSession())
        using(var tx = session.BeginTransaction())
        {
            var prices = session
                .CreateQuery("from Price p where p.ValidFrom >= :currentDate and :currentDate <= p.ValidTo")
                .SetParameter("currentDate", _clock.Today)
                .List<Price>();

            foreach(var price in prices)
            {
                price.UpdateLocalPriceUsing(rate);
            }
            session.Flush();
            tx.Commit();
        }
    }
}