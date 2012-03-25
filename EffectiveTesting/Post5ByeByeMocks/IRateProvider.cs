using System;

namespace EffectiveTesting.Post5ByeByeMocks
{
    public interface IRateProvider
    {
        decimal GetRateOn(DateTime date);
    }

    public class RateProviderStub : IRateProvider
    {
        public decimal Rate;

        public RateProviderStub()
        {
            Rate = 32.5m;
        }

        public decimal GetRateOn(DateTime date)
        {
            return Rate;
        }
    }
}