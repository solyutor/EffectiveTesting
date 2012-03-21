using System;

namespace EffectiveTesting.Post5ByeByeMocks
{
    public interface IClock
    {
        DateTime Today {get;}
    
        DateTime Now {get;}
    }
}