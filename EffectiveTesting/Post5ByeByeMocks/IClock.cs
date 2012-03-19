using System;

public interface IClock
{
    DateTime Today {get;}
    
    DateTime Now {get;}
}

public class Clock : IClock
{
    public enum ClockMode 
    {
        RealTime, 
        FixedTime
    }
    
    public readonly ClockMode Mode;
    
    private readonly object _latch;
    private DateTime? _freezedValue;

    public static readonly Clock RealTime;
    public static readonly Clock FixedTime;

    static Clock()
    {
        RealTime = new Clock();
        FixedTime = new Clock(ClockMode.FixedTime);
    }

    public Clock() : this(ClockMode.RealTime) 
    {
    }

    public Clock(ClockMode mode)
    {
        Mode = mode;
        _latch = new object();
    }

    public DateTime Today
    {
        get { return GetNow().Date;}
    }
    
    public DateTime Now 
    {
        get { return GetNow();}
    }
    
    public void SetCurrentTime(DateTime value)
    {
        SetFreezedValue(value);
    }

    public void NextValue()
    {
        SetFreezedValue(null);
    }

    private DateTime GetNow()
    {
        if(Mode == ClockMode.RealTime) return DateTime.Now;
            
        lock(_latch)
        {
            if(_freezedValue.HasValue == false)
            {
                SetFreezedValue(DateTime.Now);
            }
            return _freezedValue.Value;
        }
    }
    
    private void EnsureFixedMode()
    {
        if(Mode == ClockMode.RealTime)
            throw new InvalidOperationException("This operation is possible in Fixed mode only.");
    }
    
    public void SetFreezedValue(DateTime? value)
    {
        EnsureFixedMode();
        lock(_latch)
        {
            _freezedValue = value;
        }
    }
}