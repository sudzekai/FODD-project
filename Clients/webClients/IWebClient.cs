namespace Clients.webClients
{
    public interface IWebClient
    {
        Task<T?> DeleteAsync<T>(string url, string? token = null, CancellationToken ct = default);
        Task<T?> GetAsync<T>(string url, string? token = null, CancellationToken ct = default);
        Task<T?> PostAsync<T>(string url, object? body, string? token = null, CancellationToken ct = default);
        Task<T?> PutAsync<T>(string url, object? body, string? token = null, CancellationToken ct = default);
        void SetBaseUrl(string baseUrl);
    }
}