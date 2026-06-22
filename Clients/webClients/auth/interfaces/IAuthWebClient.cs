using Shared.requests;
using Shared.responses;

namespace Clients.webClients.auth.interfaces
{
    public interface IAuthWebClient
    {
        Task<AuthResponse> LoginAsync(AuthRequest dto, string token = null, CancellationToken ct = default);
    }
}