using Enhima;
using NHibernate;

namespace EffectiveTesting.Post5ByeByeMocks
{
    public interface ISessionManager
    {
        ISession OpenSession();

        IStatelessSession OpenStatelessSession();
    }

    public class EnhimaSessionManager : ISessionManager
    {
        private readonly SQLiteInMemoryTestHelper _testHelper;

        public EnhimaSessionManager(SQLiteInMemoryTestHelper testHelper)
        {
            _testHelper = testHelper;
        }

        public ISession OpenSession()
        {
            return _testHelper.OpenSession();
        }

        public IStatelessSession OpenStatelessSession()
        {
            return _testHelper.OpenStatelessSession();
        }
    }
}