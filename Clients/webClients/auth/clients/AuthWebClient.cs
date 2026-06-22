using Clients.webClients.auth.interfaces;
using Shared.requests;
using Shared.responses;

namespace Clients.webClients.auth.clients
{
    /// <summary>
    /// Клиент для работы с API авторизации.
    /// </summary>
    public class AuthWebClient : IAuthWebClient
    {
        private const string _base = "/login";

        private readonly IWebClient _client;

        public AuthWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<AuthResponse> LoginAsync(AuthRequest dto, string token = null, CancellationToken ct = default)
            => _client.PostAsync<AuthResponse>(_base, dto, token, ct);
    }
}