using Moq;
using NHibernate;
using NUnit.Framework;
using SharpTestsEx;

namespace EffectiveTesting.Post4Mocking
{
    [TestFixture]
    public class OrderListPresenterFixture
    {
[Test] 
public void On_start_puts_in_view_some_orders_behaviour_style()
{
    //Создаём двойник интерфейса IOrderView
    var viewMock = new Mock<IOrderView>();

    var order = new Order(10);
    //Создаём двойник интерфейса ISession
    var sessionMock = new Mock<ISession>();
    sessionMock
        //При вызове метода Get<Order> с аргументов 10
        .Setup(x => x.Get<Order>(It.Is<long>(id => id.Equals(10))))
        //И вернём 
        .Returns(order);

    var presenter = new OrderPresenter(sessionMock.Object, viewMock.Object);

    presenter.Start(10);

    viewMock.VerifySet(x => x.Visible = true);
    viewMock.VerifySet(x => x.Order = order);
}
        
[Test] 
public void On_start_puts_in_view_some_orders_state_style()
{
    var viewMock = new Mock<IOrderView>();
    //Устанавливаем поведение всех свойств по-умолчанию. 
    viewMock.SetupAllProperties();

    var order = new Order(10);

    var sessionMock = new Mock<ISession>();
    sessionMock
        //При вызове метода Get<Order> с аргументов 10
        .Setup(x => x.Get<Order>(It.Is<long>(id => id.Equals(10))))
        //И вернём 
        .Returns(order);

    var presenter = new OrderPresenter(sessionMock.Object, viewMock.Object);

    presenter.Start(10);

    viewMock.Object.Satisfy(view =>
                            view.Visible == true &&
                            view.Order == order);
}
    }
}