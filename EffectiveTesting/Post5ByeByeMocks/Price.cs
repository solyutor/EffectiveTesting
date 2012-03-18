using System;

public class Price : Enhima.Entity
{
    private decimal _amount;
    private decimal _localAmount;

    protected Price()
    {
        
    }
    
    public Price(decimal _amount)
    {
        this._amount = _amount;
    }
    
    public virtual decimal Amount
    {
        get{return _amount;}
    }
    
    public virtual decimal LocalAmount 
    {
        get{ return _localAmount;}
    }
    
    public virtual  DateTime ValidFrom {get;set;}
    
    public virtual DateTime ValidTo{get;set;}
    
    public virtual void UpdateLocalPriceUsing(decimal rate)
    {
        _localAmount = _amount * rate;
    }
}