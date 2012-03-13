using System.Collections.Generic;
using NHibernate;

namespace EffectiveTesting.Post4Mocking
{
    public class Order
    {
        public long Id { get; private set; }

        public Order(long id)
        {
            Id = id;
        }

        //skipped
    }

    public interface IOrderView
    {
        Order Order { get; set; }
        bool Visible { get; set; }
    }
    
    public class OrderPresenter
    {
        private readonly ISession _session;
        private readonly IOrderView _view;

        public OrderPresenter(ISession session, IOrderView view)
        {
            _session = session;
            _view = view;
        }

        public void Start(long id)
        {
            _view.Order = _session.Get<Order>(id);
            _view.Visible = true;
        }
    }
}