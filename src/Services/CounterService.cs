using MyApp.Proxies;

namespace MyApp.Services;

public interface ICounterService
{
    public Task IncrementCounter();
    public Task DecrementCounter();
    public Task<int> GetCount();
}

public class CounterService : ICounterService
{
    private readonly ICounterProxy _proxy;

    public CounterService(ICounterProxy proxy)
    {
        _proxy = proxy;
    }

    public async Task<int> GetCount()
    {
        return await _proxy.GetCount();
    }

    public async Task IncrementCounter()
    {
        await _proxy.ChangeCounter(1);
    }

    public async Task DecrementCounter()
    {
        await _proxy.ChangeCounter(-1);
    }
}