using Clients.helpers;
using Clients.webClients.products.interfaces;
using Shared.dtos.products;
using Shared.requests;

namespace Clients.webClients.products.clients
{
    /// <summary>
    /// Веб-клиент для работы с ресурсом "товары" на сервере.
    /// Обёртка над статическим `WebClient`, формирует URL по базовому пути `/products`
    /// и предоставляет асинхронные методы для CRUD-операций и получения метаданных.
    /// Все методы выполняют HTTP-запросы и при ошибках проксируют исключения от `WebClient`.
    /// </summary>
    public class ProductsWebClient : IProductsWebClient
    {
        /// <summary>
        /// Базовый путь для всех запросов этого клиента (префикс эндпоинтов).
        /// </summary>
        private const string _base = "/products";

        /// <summary>
        /// Получить список товаров с учётом параметров фильтрации/пагинации из запроса.
        /// </summary>
        /// <param name="req">Параметры запроса: фильтры, сортировка, страница и размер страницы.</param>
        /// <returns>Список упрощённых DTO товаров (<see cref="ProductSimpleDTO"/>).</returns>
        public async Task<List<ProductSimpleDTO>> GetProductsAsync(GetProductsListRequest req)
            => await WebClient.GetAsync<List<ProductSimpleDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}"
            ) ?? [];

        /// <summary>
        /// Получить полную информацию о товаре по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <returns>DTO с полной информацией о товаре (<see cref="ProductDTO"/>).</returns>
        public async Task<ProductDTO> GetProductByIdAsync(int id)
            => await WebClient.GetAsync<ProductDTO>($"{_base}/{id}");

        /// <summary>
        /// Создать новый товар на сервере.
        /// </summary>
        /// <param name="dto">Модель для создания товара (<see cref="ProductCreateDTO"/>).</param>
        /// <returns>
        /// DTO созданного товара (<see cref="ProductDTO"/>). Может быть <c>null</c>, если сервер вернул пустой ответ.
        /// </returns>
        public async Task<ProductDTO?> CreateProductAsync(ProductCreateDTO dto)
            => await WebClient.PostAsync<ProductDTO>(_base, dto);

        /// <summary>
        /// Обновить основную информацию о товаре (без цен и связей).
        /// </summary>
        /// <param name="id">Идентификатор обновляемого товара.</param>
        /// <param name="dto">Модель обновления (<see cref="ProductUpdateDTO"/>).</param>
        /// <returns>Асинхронная задача, завершающаяся по окончании операции.</returns>
        public async Task UpdateProductAsync(int id, ProductUpdateDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}", dto);

        /// <summary>
        /// Обновить ценовую информацию товара.
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <param name="dto">Модель обновления цен (<see cref="ProductPricingUpdateDTO"/>).</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task UpdateProductPricingAsync(int id, ProductPricingUpdateDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}/pricing", dto);

        /// <summary>
        /// Обновить связи товара с другими сущностями (категории, теги, сопутствующие товары и т.д.).
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <param name="dto">Модель для обновления связей (<see cref="ProductRelationsUpdateDTO"/>).</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task UpdateProductRelationsAsync(int id, ProductRelationsUpdateDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}/relations", dto);

        /// <summary>
        /// Удалить товар по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого товара.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task DeleteProductAsync(int id)
            => await WebClient.DeleteAsync<object?>($"{_base}/{id}");

        /// <summary>
        /// Получить общее количество товаров в системе.
        /// </summary>
        /// <returns>Количество товаров (целое число).</returns>
        public async Task<int> GetProductsCountAsync()
            => await WebClient.GetAsync<int>($"{_base}/count");
    }
}