using Clients.helpers;
using Clients.webClients.users.interfaces;
using Shared.dtos.users;
using Shared.requests;

namespace Clients.webClients.users.clients
{
    /// <summary>
    /// HTTP-клиент для работы с пользователями.
    /// </summary>
    public class UsersWebClient : IUsersWebClient
    {
        private const string _endpointBase = "/users";

        private readonly IWebClient _client;

        public UsersWebClient(IWebClient client)
        {
            _client = client;
        }

        public Task<List<UserSimpleDTO>> GetUsersAsync(GetListRequest req, string token, CancellationToken ct = default)
            => _client.GetAsync<List<UserSimpleDTO>>(
                $"{_endpointBase}{QueryBuilder.ToQueryString(req)}",
                token,
                ct);

        public Task<UserDTO> GetUserByIdAsync(int id, string token, CancellationToken ct = default)
            => _client.GetAsync<UserDTO>($"{_endpointBase}/{id}", token, ct);

        public Task<UserDTO?> CreateUserAsync(UserCreateDTO dto, string token, CancellationToken ct = default)
            => _client.PostAsync<UserDTO>(_endpointBase, dto, token, ct);

        public Task UpdateUserAsync(int id, UserUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_endpointBase}/{id}", dto, token, ct);

        public Task UpdateUserPasswordAsync(int id, UserPasswordUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_endpointBase}/{id}/password", dto, token, ct);

        public Task UpdateUserRoleAsync(int id, UserRoleUpdateDTO dto, string token, CancellationToken ct = default)
            => _client.PutAsync<object?>($"{_endpointBase}/{id}/role", dto, token, ct);

        public Task DeleteUserAsync(int id, string token, CancellationToken ct = default)
            => _client.DeleteAsync<object?>($"{_endpointBase}/{id}", token, ct);

        public Task<int> GetUsersCountAsync(string token, CancellationToken ct = default)
            => _client.GetAsync<int>($"{_endpointBase}/count", token, ct);
    }
}