using MyApp.Models;

namespace MyApp.Proxies;

public interface ICounterProxy
{
    Task<List<Counter>> GetCounters();
    Task<Counter> GetCounter(int counterId);

    Task ChangeCounter(int counterId, int value);

    Task CreateCounter();
    Task DeleteCounter(int counterId);
}

public class CounterProxy : ICounterProxy
{
    private readonly HttpClient _client;

    public CounterProxy(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<Counter>> GetCounters()
    {
        var route = $"counters";
        var response = await _client.GetAsync(route);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<Counter>>();
    }

    public async Task<Counter> GetCounter(int counterId = 1)
    {
        var route = $"counters/{counterId}";
        var response = await _client.GetAsync(route);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Counter>();
    }

    public async Task ChangeCounter(int counterId, int value)
    {
        var route = $"counters/{counterId}";

        var response = await _client.PostAsJsonAsync(route, new {
            Value = value
        });
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateCounter()
    {
        var route = "counters/new";

        var response = await _client.PostAsync(route, null);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCounter(int counterId)
    {
        var route = $"counters/{counterId}";

        var response = await _client.DeleteAsync(route);
        response.EnsureSuccessStatusCode();
    }
}