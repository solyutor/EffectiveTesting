using System;

public interface ITask
{
    void Run();
}

public interface IRateProvider
{
    decimal GetRateOn(DateTime date);
}