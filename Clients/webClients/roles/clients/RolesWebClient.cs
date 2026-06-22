using Clients.helpers;
using Clients.webClients.roles.interfaces;
using Shared.dtos.roles;
using Shared.requests;

namespace Clients.webClients.roles.clients
{
    /// <summary>
    /// HTTP-клиент для работы с ролями.
    /// </summary>
    public class RolesWebClient : IRolesWebClient
    {
        private const string _base = "/roles";

        private readonly IWebClient _client;

        public RolesWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<RoleDTO>> GetRolesAsync(GetListRequest req, string token, CancellationToken ct = default)
            => _client.GetAsync<List<RoleDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}",
                token,
                ct);

        public Task<RoleDTO> GetRoleByIdAsync(int id, string token, CancellationToken ct = default)
            => _client.GetAsync<RoleDTO>($"{_base}/{id}", token, ct);

        public Task<int> GetRolesCountAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_base}/count", token, ct);
    }
}