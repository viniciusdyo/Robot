using System.Net.Http.Json;
using System.Text.Json;

namespace Robot;

public class RequestProvider
{
    private readonly Lazy<HttpClient> _httpClient = new (() =>
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;
    }, LazyThreadSafetyMode.ExecutionAndPublication);

    public async Task<TResult?> GetAsync<TResult>(string url)
    {
        var httpClient = _httpClient.Value;
        var response = await httpClient.GetAsync(url).ConfigureAwait(false);

        var result = await response.Content.ReadFromJsonAsync<TResult>();

        return result;
    }

    public async Task<TResult?> PutAsync<TResult>(string url, TResult data)
    {
        var httpClient = _httpClient.Value;
        var content = new StringContent(JsonSerializer.Serialize(data));
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        var response = await httpClient.PutAsync(url, content).ConfigureAwait(false);

        return await response.Content.ReadFromJsonAsync<TResult>();
    }
}
