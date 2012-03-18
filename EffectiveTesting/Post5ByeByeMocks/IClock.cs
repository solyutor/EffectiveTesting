using System;

public interface IClock
{
    DateTime Today {get;}
    
    DateTime Now {get;}
}

public class Clock : IClock
{
    public enum Mode 
    {
        Normal, 
        Freezed
    }
    
    private Mode _mode;
    private DateTime? _freezedValue;
    
    private static readonly Clock Default;
    private static readonly Clock Freezed;
    
    static Clock()
    {
        Default = new Clock();
        Freezed = new Clock(Mode.Freezed);
    }
    
    public Clock() : this(Mode.Normal)
    {
        
    }
    
    public Clock(Mode mode)
    {
        _mode = mode;
    }
    
    public DateTime Today
    {
        get { return GetNow().Date;}
    }
    
    public DateTime Now 
    {
        get { return GetNow();}
    }
    
    private DateTime GetNow()
    {
        if(_mode == Clock.Mode.Normal) return DateTime.Now;
            
        if(_freezedValue.HasValue == false)
        {
            _freezedValue = DateTime.Now;
        }
        
        return _freezedValue.Value;
    }
}