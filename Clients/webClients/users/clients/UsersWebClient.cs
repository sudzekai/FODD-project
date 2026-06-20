using Clients.helpers;
using Clients.webClients.users.interfaces;
using Shared.dtos.users;
using Shared.requests;

namespace Clients.webClients.users.clients
{
    /// <summary>
    /// HTTP-клиент для работы с базовыми эндпоинтами сущности "пользователь".
    /// </summary>
    /// <remarks>
    /// Построение URL происходит относительно базового пути <c>/users</c>.
    /// Все методы асинхронны и делегируют выполнение общему <c>WebClient</c>.
    /// При сетевых ошибках или ошибках сериализации могут быть выброшены исключения, возникающие внутри <c>WebClient</c>.
    /// </remarks>
    public class UsersWebClient : IUsersWebClient
    {
        private const string _endpointBase = "/users";

        /// <summary>
        /// Получить список пользователей с возможностью фильтрации, сортировки и пагинации.
        /// </summary>
        /// <param name="req">Объект запроса, содержащий параметры фильтрации, сортировки и страницы.</param>
        /// <returns>Список DTO с базовыми данными пользователей.</returns>
        /// <remarks>Выполняет GET запрос к <c>/users{query}</c>, где <c>{query}</c> — результат <c>QueryBuilder.ToQueryString(req)</c>.</remarks>
        public async Task<List<UserSimpleDTO>> GetUsersAsync(GetListRequest req)
            => await WebClient.GetAsync<List<UserSimpleDTO>>($"{_endpointBase}{QueryBuilder.ToQueryString(req)}");

        /// <summary>
        /// Получить полную информацию о пользователе по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>DTO с полными данными пользователя.</returns>
        /// <remarks>Выполняет GET запрос к <c>/users/{id}</c>.</remarks>
        public async Task<UserDTO> GetUserByIdAsync(int id)
            => await WebClient.GetAsync<UserDTO>($"{_endpointBase}/{id}");

        /// <summary>
        /// Создать нового пользователя.
        /// </summary>
        /// <param name="dto">DTO с данными для создания пользователя.</param>
        /// <returns>DTO созданного пользователя или <c>null</c>, если сервер вернул отсутствующий/неожиданный ответ.</returns>
        /// <remarks>Выполняет POST запрос к <c>/users</c> с телом запроса <paramref name="dto"/>.</remarks>
        public async Task<UserDTO?> CreateUserAsync(UserCreateDTO dto)
            => await WebClient.PostAsync<UserDTO>(_endpointBase, dto);

        /// <summary>
        /// Обновить данные пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя для обновления.</param>
        /// <param name="dto">DTO с обновляемыми полями.</param>
        /// <returns>Асинхронная задача, завершающаяся после выполнения запроса.</returns>
        /// <remarks>Выполняет PUT запрос к <c>/users/{id}</c> с телом запроса <paramref name="dto"/>.</remarks>
        public async Task UpdateUserAsync(int id, UserUpdateDTO dto)
            => await WebClient.PutAsync<object?>($"{_endpointBase}/{id}", dto);

        /// <summary>
        /// Обновить пароль пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="dto">DTO с информацией для смены пароля (старый и новый пароли и т.п.).</param>
        /// <returns>Асинхронная задача, завершающаяся после выполнения запроса.</returns>
        /// <remarks>Выполняет PUT запрос к <c>/users/{id}/password</c>.</remarks>
        public async Task UpdateUserPasswordAsync(int id, UserPasswordUpdateDTO dto)
            => await WebClient.PutAsync<object?>($"{_endpointBase}/{id}/password", dto);

        /// <summary>
        /// Обновить роль пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="dto">DTO с новой ролью или набором ролей.</param>
        /// <returns>Асинхронная задача, завершающаяся после выполнения запроса.</returns>
        /// <remarks>Выполняет PUT запрос к <c>/users/{id}/role</c>.</remarks>
        public async Task UpdateUserRoleAsync(int id, UserRoleUpdateDTO dto)
            => await WebClient.PutAsync<object?>($"{_endpointBase}/{id}/role", dto);

        /// <summary>
        /// Удалить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя для удаления.</param>
        /// <returns>Асинхронная задача, завершающаяся после выполнения запроса.</returns>
        /// <remarks>Выполняет DELETE запрос к <c>/users/{id}</c>.</remarks>
        public async Task DeleteUserAsync(int id)
            => await WebClient.DeleteAsync<object?>($"{_endpointBase}/{id}");

        /// <summary>
        /// Получить общее количество пользователей.
        /// </summary>
        /// <returns>Целое число — количество пользователей в системе.</returns>
        /// <remarks>Выполняет GET запрос к <c>/users/count</c>.</remarks>
        public async Task<int> GetUsersCountAsync()
            => await WebClient.GetAsync<int>($"{_endpointBase}/count");
    }
}