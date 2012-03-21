using System;

namespace EffectiveTesting.Post5ByeByeMocks
{
public class Clock : IClock
{
    public enum ClockMode 
    {
        RealTime, 
        FixedTime
    }
    
    /// <summary>
    /// Current ClockMode
    /// </summary>
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

    /// <summary>
    /// Creates new instance of <see cref="Clock"/> using realtime mode. 
    /// </summary>
    public Clock() : this(ClockMode.RealTime) 
    {
    }

    /// <summary>
    /// Creates new instance of <see cref="Clock"/> using supplied mode. 
    /// </summary>
    public Clock(ClockMode mode)
    {
        Mode = mode;
        _latch = new object();
    }

    /// <summary>
    /// Returns current date.
    /// </summary>
    public DateTime Today
    {
        get { return GetNow().Date;}
    }
    
    /// <summary>
    /// Returns current date and time. 
    /// </summary>
    public DateTime Now 
    {
        get { return GetNow();}
    }
    
    /// <summary>
    /// Sets user supplied datetime as current time.
    /// </summary>
    public void SetCurrentTime(DateTime value)
    {
        SetFreezedValue(value);
    }

    /// <summary>
    /// Forces <see cref="Clock"/> to pick and fix current datetime at the next call to Now or Today properties.
    /// </summary>
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

    private void SetFreezedValue(DateTime? value)
    {
        EnsureFixedMode();
        lock(_latch)
        {
            _freezedValue = value;
        }
    }
}
}