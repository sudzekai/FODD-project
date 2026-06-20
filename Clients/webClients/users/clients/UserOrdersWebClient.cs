using Clients.webClients.users.interfaces;
using Shared.dtos.orders;

namespace Clients.webClients.users.clients
{
    /// <summary>
    /// Клиент для обращения к эндпоинтам пользователей, связанным с заказами.
    /// </summary>
    /// <remarks>
    /// Формирует относительные пути вида <c>/users/{userId}/orders</c> и
    /// делегирует выполнение HTTP-запросов реализации <c>WebClient</c>.
    /// Все методы асинхронны и возвращают DTO, полученные от удалённого API.
    /// </remarks>
    public class UserOrdersWebClient : IUserOrdersWebClient
    {
        /// <summary>
        /// Базовый путь для всех запросов этого клиента (префикс эндпоинтов).
        /// </summary>
        private const string _base = "/users";

        /// <summary>
        /// Асинхронно получает список заказов указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, заказы которого нужно получить.</param>
        /// <returns>Список <see cref="OrderDTO"/> — заказы пользователя.</returns>
        /// <remarks>
        /// В случае сетевой ошибки или ошибки сервера поведение зависит от реализации <c>WebClient</c>
        /// (возможен выброс исключения).
        /// </remarks>
        public async Task<List<OrderDTO>> GetUserOrdersAsync(int userId)
            => await WebClient.GetAsync<List<OrderDTO>>($"{_base}/{userId}/orders");

        /// <summary>
        /// Асинхронно получает количество заказов указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Целое число — количество заказов.</returns>
        public async Task<int> GetUserOrdersCountAsync(int userId)
            => await WebClient.GetAsync<int>($"{_base}/{userId}/orders/count");

        /// <summary>
        /// Асинхронно создаёт новый заказ для указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, для которого создаётся заказ.</param>
        /// <returns>Созданный <see cref="OrderDTO"/> с информацией о заказе.</returns>
        /// <remarks>
        /// Текущая реализация отправляет пустое тело запроса (<c>null</c>) в POST.
        /// При необходимости добавьте параметры или DTO в тело запроса.
        /// </remarks>
        public async Task<OrderDTO> CreateUserOrderAsync(int userId)
            => await WebClient.PostAsync<OrderDTO>($"{_base}/{userId}/orders", null);
    }
}