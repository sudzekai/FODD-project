using Clients.webClients.orders.interfaces;
using Shared.dtos.orders;

namespace Clients.webClients.orders.clients
{
    /// <summary>
    /// HTTP-клиент для взаимодействия с API по товарам в заказах.
    /// </summary>
    /// <remarks>
    /// Все методы асинхронные и используют общий базовый путь <c>"/orders"</c>.
    /// Для выполнения HTTP-запросов применяется вспомогательный класс <c>WebClient</c>.
    /// Реальная семантика операций (например, разница между <c>remove</c> и <c>delete</c>) определяется серверной реализацией API.
    /// </remarks>
    public class OrderProductsWebClient : IOrderProductsWebClient
    {
        private const string _base = "/orders";

        /// <summary>
        /// Получает список товаров для указанного заказа.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <returns>Список DTO товаров заказа (<see cref="OrderProductDTO"/>).</returns>
        /// <remarks>Выполняет GET-запрос к <c>/orders/{orderId}/products</c>.</remarks>
        public async Task<List<OrderProductDTO>> GetOrderProductsAsync(int orderId)
            => await WebClient.GetAsync<List<OrderProductDTO>>($"{_base}/{orderId}/products");

        /// <summary>
        /// Получает количество (число позиций) товаров в указанном заказе.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <returns>Целое число — количество позиций товаров в заказе.</returns>
        /// <remarks>Выполняет GET-запрос к <c>/orders/{orderId}/products/count</c>.</remarks>
        public async Task<int> GetOrderProductsCountAsync(int orderId)
            => await WebClient.GetAsync<int>($"{_base}/{orderId}/products/count");

        /// <summary>
        /// Получает суммарное количество единиц товаров в указанном заказе.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <returns>Целое число — суммарное количество единиц товаров.</returns>
        /// <remarks>Выполняет GET-запрос к <c>/orders/{orderId}/products/sum</c>.</remarks>
        public async Task<int> GetOrderProductsSumAsync(int orderId)
            => await WebClient.GetAsync<int>($"{_base}/{orderId}/products/sum");

        /// <summary>
        /// Добавляет (или увеличивает количество) товар в указанном заказе.
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="dto">DTO с данными о добавляемом товаре (<see cref="OrderProductUpdateDTO"/>).</param>
        /// <returns>Асинхронная задача. Результат запроса игнорируется.</returns>
        /// <remarks>Выполняет POST-запрос к <c>/orders/{orderId}/products/add</c>. Возможны сетевые исключения при ошибках соединения.</remarks>
        public async Task AddOrderProductAsync(int orderId, OrderProductUpdateDTO dto)
            => await WebClient.PostAsync<object?>($"{_base}/{orderId}/products/add", dto);

        /// <summary>
        /// Уменьшает количество товара в заказе или помечает его как удалённый (семантика зависит от API).
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="dto">DTO с данными об удаляемом товаре (<see cref="OrderProductUpdateDTO"/>).</param>
        /// <returns>Асинхронная задача. Результат запроса игнорируется.</returns>
        /// <remarks>Выполняет POST-запрос к <c>/orders/{orderId}/products/remove</c>.</remarks>
        public async Task RemoveOrderProductAsync(int orderId, OrderProductUpdateDTO dto)
            => await WebClient.PostAsync<object?>($"{_base}/{orderId}/products/remove", dto);

        /// <summary>
        /// Удаляет товар из заказа (операция удаления на стороне сервера).
        /// </summary>
        /// <param name="orderId">Идентификатор заказа.</param>
        /// <param name="dto">DTO с данными об удаляемом товаре (<see cref="OrderProductUpdateDTO"/>).</param>
        /// <returns>Асинхронная задача. Результат запроса игнорируется.</returns>
        /// <remarks>Выполняет POST-запрос к <c>/orders/{orderId}/products/delete</c>.</remarks>
        public async Task DeleteOrderProductAsync(int orderId, OrderProductUpdateDTO dto)
            => await WebClient.PostAsync<object?>($"{_base}/{orderId}/products/delete", dto);
    }
}