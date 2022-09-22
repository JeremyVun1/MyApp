using MyApp.Models;

namespace MyApp.Proxies;

public interface ICounterProxy
{
    public Task<int> GetCount();
    public Task ChangeCounter(int change);
}

public class CounterProxy : ICounterProxy
{
    private readonly HttpClient _client;

    public CounterProxy(HttpClient client)
    {
        _client = client;
    }

    public async Task<int> GetCount()
    {
        var response = await _client.GetStringAsync("counter");
        return int.Parse(response);
    }

    public async Task ChangeCounter(int change)
    {
        var request = new ChangeCountRequest
        {
            Value = change
        };
        await _client.PostAsJsonAsync("counter", request);
    }
}