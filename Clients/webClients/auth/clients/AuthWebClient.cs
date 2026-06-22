using Clients.webClients.auth.interfaces;
using Shared.requests;
using Shared.responses;

namespace Clients.webClients.auth.clients
{
    public class AuthWebClient : IAuthWebClient
    {
        private const string _base = "/login";

        public async Task<AuthResponse> LoginAsync(AuthRequest dto)
            => await WebClient.PostAsync<AuthResponse>(_base, dto);
    }
}
