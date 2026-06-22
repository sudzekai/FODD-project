using Shared.responses;
using Shared.types.exceptions;
using System.Net.Http.Json;
using System.Text.Json;

namespace Clients.webClients
{
    /// <summary>
    /// HTTP-клиент сервиса API.
    /// Экземпляр создаётся для каждого клиента и не является статическим.
    /// </summary>
    public class WebClient : IWebClient
    {
        private readonly HttpClient _httpClient;

        public WebClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public void SetBaseUrl(string baseUrl)
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public Task<T?> GetAsync<T>(string url, string? token = null, CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Get, url, null, token, ct);

        public Task<T?> DeleteAsync<T>(string url, string? token = null, CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Delete, url, null, token, ct);

        public Task<T?> PostAsync<T>(string url, object? body, string? token = null, CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Post, url, body, token, ct);

        public Task<T?> PutAsync<T>(string url, object? body, string? token = null, CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Put, url, body, token, ct);

        private async Task<T> SendAsync<T>(
            HttpMethod method,
            string url,
            object? body,
            string? token,
            CancellationToken ct)
        {
            using var request = new HttpRequestMessage(method, url);

            if (!string.IsNullOrWhiteSpace(token))
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            if (body != null)
                request.Content = JsonContent.Create(body);

            using var response = await _httpClient.SendAsync(request, ct);

            ResponseEnvelope<T>? envelope;

            try
            {
                envelope = await response.Content.ReadFromJsonAsync<ResponseEnvelope<T>>(cancellationToken: ct);
            }
            catch (Exception ex)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    throw new ApiException("Доступ запрещён", 403);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new ApiException("Требуется авторизация", 401);

                throw new ApiException($"Неверный формат ответа сервера: {await response.Content.ReadAsStringAsync()}");
            }

            if (!response.IsSuccessStatusCode)
                throw new ApiException(envelope?.Error?.Message ?? "Ошибка сервера",
                                        envelope?.Error?.Code);

            if (envelope is null)
                throw new ApiException("Пустой ответ сервера");

            if (!envelope.IsSuccess)
                throw new ApiException(envelope.Error?.Message ?? "Ошибка сервера",
                                        envelope.Error?.Code);

            return envelope.Data!;
        }
    }
}