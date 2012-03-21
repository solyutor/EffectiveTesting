using System;

namespace EffectiveTesting.Post5ByeByeMocks
{
    public interface IRateProvider
    {
        decimal GetRateOn(DateTime date);
    }
}