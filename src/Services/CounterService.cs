using MyApp.Models;
using MyApp.Proxies;

namespace MyApp.Services;

public interface ICounterService
{
    Task IncrementCounter(int counterId = 1);
    Task DecrementCounter(int counterId = 1);
    Task<Counter> GetCounter(int counterId = 1);
    Task<List<Counter>> GetCounters();
    
    Task<bool> CreateCounter();
    Task<bool> DeleteCounter(int counterId);
}

public class CounterService : ICounterService
{
    private readonly ICounterProxy _proxy;

    public CounterService(ICounterProxy proxy)
    {
        _proxy = proxy;
    }

    public async Task<Counter> GetCounter(int counterId)
    {
        return await _proxy.GetCounter(counterId);
    }

    public async Task IncrementCounter(int counterId)
    {
        await _proxy.ChangeCounter(counterId, 1);
    }

    public async Task DecrementCounter(int counterId)
    {
        await _proxy.ChangeCounter(counterId, -1);
    }

    public async Task<List<Counter>> GetCounters()
    {
        return await _proxy.GetCounters();
    }

    public async Task<bool> CreateCounter()
    {
        try
        {
            await _proxy.CreateCounter();
            return true;
        }
        catch (Exception) {}

        return false;
    }

    public async Task<bool> DeleteCounter(int counterId)
    {
        try
        {
            await _proxy.DeleteCounter(counterId);
            return true;
        }
        catch (Exception) {}
        
        return false;
    }
}