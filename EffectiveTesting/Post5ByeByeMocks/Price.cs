using System;

namespace EffectiveTesting.Post5ByeByeMocks
{
    public class Price : Enhima.Entity
    {
        private decimal _amount;
        private decimal _localAmount;

        protected Price()
        {
            LastUpdated = DateTime.Now;
        }
    
        public Price(decimal amount, DateTime lastUpdated)
        {
            _amount = amount;   
            LastUpdated = lastUpdated;
        }

        public virtual decimal Amount
        {
            get{ return _amount; }
        }
    
        public virtual decimal LocalAmount 
        {
            get{ return _localAmount; }
        }
    
        public virtual  DateTime ValidFrom {get;set;}
    
        public virtual DateTime ValidTo{get;set;}

        public virtual DateTime LastUpdated { get; set; }

        public virtual void UpdateLocalPriceUsing(decimal rate)
        {
            _localAmount = _amount * rate;
        }
    }
}