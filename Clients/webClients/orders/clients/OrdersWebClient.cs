using Clients.helpers;
using Clients.webClients.orders.interfaces;
using Shared.dtos.orders;
using Shared.requests;

namespace Clients.webClients.orders.clients
{
    /// <summary>
    /// Клиент для работы с API заказов.
    /// Обёртка над общим `WebClient`, формирующая URL-ы для эндпоинтов, связанных с сущностью заказа.
    /// </summary>
    public class OrdersWebClient : IOrdersWebClient
    {
        private const string _base = "/orders";

        /// <summary>
        /// Получить список заказов с параметрами фильтрации/пагинации.
        /// </summary>
        /// <param name="req">Параметры запроса списка (фильтры, страница, размер страницы и т.п.).</param>
        /// <returns>Список DTO заказов, соответствующих условиям запроса.</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">В случае ошибки HTTP-запроса к API.</exception>
        public async Task<List<OrderDTO>> GetOrdersAsync(GetListRequest req)
            => await WebClient.GetAsync<List<OrderDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}"
            );

        /// <summary>
        /// Получить детали заказа по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор заказа.</param>
        /// <returns>Объект <see cref="OrderDTO"/> с подробной информацией о заказе.</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">В случае ошибки HTTP-запроса к API.</exception>
        public async Task<OrderDTO> GetOrderByIdAsync(int id)
            => await WebClient.GetAsync<OrderDTO>($"{_base}/{id}");

        /// <summary>
        /// Обновить статус заказа.
        /// </summary>
        /// <param name="id">Идентификатор заказа для обновления.</param>
        /// <param name="dto">DTO с данными для обновления статуса.</param>
        /// <returns>Асинхронная задача. Результат не возвращается.</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">В случае ошибки HTTP-запроса к API.</exception>
        public async Task UpdateOrderStatusAsync(int id, OrderStatusUpdateDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}/status", dto);

        /// <summary>
        /// Обновить информацию о доставке заказа.
        /// </summary>
        /// <param name="id">Идентификатор заказа для обновления.</param>
        /// <param name="dto">DTO с данными доставки (адрес, дата, служба и т.п.).</param>
        /// <returns>Асинхронная задача. Результат не возвращается.</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">В случае ошибки HTTP-запроса к API.</exception>
        public async Task UpdateOrderDeliveryAsync(int id, OrderDeliveryUpdateDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}/delivery", dto);

        /// <summary>
        /// Удалить заказ по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого заказа.</param>
        /// <returns>Асинхронная задача. Результат не возвращается.</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">В случае ошибки HTTP-запроса к API.</exception>
        public async Task DeleteOrderAsync(int id)
            => await WebClient.DeleteAsync<object?>($"{_base}/{id}");

        /// <summary>
        /// Получить общее количество заказов.
        /// </summary>
        /// <returns>Количество заказов (целое число).</returns>
        /// <exception cref="System.Net.Http.HttpRequestException">В случае ошибки HTTP-запроса к API.</exception>
        public async Task<int> GetOrdersCountAsync()
            => await WebClient.GetAsync<int>($"{_base}/count");
    }
}