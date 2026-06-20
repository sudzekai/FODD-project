using Clients.helpers;
using Clients.webClients.products.interfaces;
using Shared.dtos.products;

namespace Clients.webClients.products.clients
{
    /// <summary>
    /// Веб-клиент для работы с тегами товаров.
    /// Предоставляет методы для получения, добавления и удаления тегов конкретного товара,
    /// а также для получения количества тегов.
    /// </summary>
    public class ProductTagsWebClient : IProductTagsWebClient
    {
        /// <summary>
        /// Базовый путь для запросов, относящихся к товарам.
        /// Комбинируется с идентификатором товара и конечными частями маршрутов.
        /// </summary>
        private const string _base = "/products";

        /// <summary>
        /// Получить список имен/значений тегов, связанных с товаром.
        /// </summary>
        /// <param name="productId">Идентификатор товара.</param>
        /// <returns>Список тегов товара в виде строк. Пустой список, если теги отсутствуют.</returns>
        /// <remarks>
        /// Выполняет GET-запрос к маршруту: <c>/_base/{productId}/tags</c>.
        /// Использует помощник <c>WebClient.GetAsync{T}</c>.
        /// </remarks>
        public async Task<List<string>> GetProductTagsAsync(int productId)
            => await WebClient.GetAsync<List<string>>($"{_base}/{productId}/tags") ?? [];

        /// <summary>
        /// Добавить набор тегов товару (batch).
        /// </summary>
        /// <param name="productId">Идентификатор товара.</param>
        /// <param name="dto">DTO с идентификаторами тегов для добавления. Свойство <c>TagIds</c> обязательно.</param>
        /// <returns>
        /// <c>true</c>, если операция успешно выполнена; <c>false</c> в противном случае.
        /// </returns>
        /// <remarks>
        /// Выполняет POST-запрос к маршруту: <c>/_base/{productId}/tags/add</c> с телом запроса <c>dto</c>.
        /// Ожидается, что API возвращает логическое значение состояния выполнения.
        /// </remarks>
        public async Task<bool> AddProductTagsAsync(int productId, ProductTagsUpdateDTO dto)
            => await WebClient.PostAsync<bool>($"{_base}/{productId}/tags/add", dto);

        /// <summary>
        /// Удалить набор тегов у товара (batch).
        /// </summary>
        /// <param name="productId">Идентификатор товара.</param>
        /// <param name="dto">DTO с идентификаторами тегов для удаления. Свойство <c>TagIds</c> обязательно.</param>
        /// <returns>
        /// <c>true</c>, если операция успешно выполнена; <c>false</c> в противном случае.
        /// </returns>
        /// <remarks>
        /// Выполняет POST-запрос к маршруту: <c>/_base/{productId}/tags/remove</c> с телом запроса <c>dto</c>.
        /// Используется "batch" подход — передаётся список id тегов.
        /// </remarks>
        public async Task<bool> RemoveProductTagsAsync(int productId, ProductTagsUpdateDTO dto)
            => await WebClient.PostAsync<bool>($"{_base}/{productId}/tags/remove", dto);

        /// <summary>
        /// Получить количество тегов, связанных с товаром.
        /// </summary>
        /// <param name="productId">Идентификатор товара.</param>
        /// <returns>Количество тегов для указанного товара.</returns>
        /// <remarks>
        /// Выполняет GET-запрос к маршруту: <c>/_base/{productId}/tags/count</c>.
        /// Полезно для отображения статистики или проверки наличия тегов без получения полного списка.
        /// </remarks>
        public async Task<int> GetProductTagsCountAsync(int productId)
            => await WebClient.GetAsync<int>($"{_base}/{productId}/tags/count");
    }
}