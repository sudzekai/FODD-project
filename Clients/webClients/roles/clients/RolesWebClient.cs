using Clients.helpers;
using Clients.webClients.roles.interfaces;
using Shared.dtos.roles;
using Shared.requests;

namespace Clients.webClients.roles.clients
{
    /// <summary>
    /// HTTP-клиент для работы с ресурсом ролей API.
    /// </summary>
    /// <remarks>
    /// Этот класс предоставляет асинхронные методы для получения списка ролей, 
    /// отдельной роли по идентификатору и общего количества ролей.
    /// Запросы выполняются через общий `WebClient`. Формирование query-string
    /// производится с помощью `QueryBuilder.ToQueryString`.
    /// Обработка ошибок и десериализация ответа делегируются реализации `WebClient`.
    /// </remarks>
    public class RolesWebClient : IRolesWebClient
    {
        private const string _base = "/roles";

        /// <summary>
        /// Асинхронно получает список ролей по заданным параметрам запроса.
        /// </summary>
        /// <param name="req">
        /// Параметры выборки (`GetListRequest`): фильтры, сортировка, пагинация и пр.
        /// Может быть <c>null</c>, если требуется получить все элементы без дополнительных параметров.
        /// </param>
        /// <returns>
        /// Список DTO ролей (`List&lt;RoleDTO&gt;`), соответствующих заданным параметрам.
        /// </returns>
        /// <exception cref="System.Net.Http.HttpRequestException">
        /// При ошибках сетевого уровня или невозможности получить корректный ответ от сервера.
        /// </exception>
        /// <remarks>
        /// Формирует конечную точку в виде "<c>/roles{query}</c>" и вызывает `WebClient.GetAsync`.
        /// </remarks>
        public async Task<List<RoleDTO>> GetRolesAsync(GetListRequest req)
            => await WebClient.GetAsync<List<RoleDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}"
            ) ?? [];

        /// <summary>
        /// Асинхронно получает роль по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор роли (ожидается положительное целое значение).</param>
        /// <returns>
        /// DTO роли (`RoleDTO`) с данными запрошенного ресурса.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Может быть выброшено, если передан некорректный `id` (в случае валидации до вызова WebClient).
        /// </exception>
        /// <exception cref="System.Net.Http.HttpRequestException">
        /// При ошибках выполнения HTTP-запроса или некорректном ответе от сервера.
        /// </exception>
        /// <remarks>
        /// Формирует путь "<c>/roles/{id}</c>" и использует `WebClient.GetAsync` для получения данных.
        /// Поведение при 404 зависит от реализации `WebClient` (например, возвращение null или выброс исключения).
        /// </remarks>
        public async Task<RoleDTO> GetRoleByIdAsync(int id)
            => await WebClient.GetAsync<RoleDTO>($"{_base}/{id}");

        /// <summary>
        /// Асинхронно получает общее количество ролей в системе.
        /// </summary>
        /// <returns>Общее количество ролей (целое число).</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">
        /// При ошибках сетевого уровня или при некорректном ответе от сервера.
        /// </exception>
        /// <remarks>
        /// Выполняет GET-запрос к конечной точке "<c>/roles/count</c>".
        /// </remarks>
        public async Task<int> GetRolesCountAsync()
            => await WebClient.GetAsync<int>($"{_base}/count");
    }
}