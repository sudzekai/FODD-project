using Clients.helpers;
using Clients.webClients.categories.interfaces;
using Shared.dtos.categories;
using Shared.requests;

namespace Clients.webClients.categories.clients
{
    /// <summary>
    /// Веб‑клиент для работы с API категорий.
    /// Инкапсулирует HTTP-вызовы к маршруту <c>/categories</c>.
    /// </summary>
    public class CategoriesWebClient : ICategoriesWebClient
    {
        private const string _base = "/categories";

        /// <summary>
        /// Получить список категорий с параметрами фильтрации/страничности.
        /// </summary>
        /// <param name="req">Параметры запроса списка (фильтры, сортировка, постраничность).</param>
        /// <returns>Список DTO категорий, соответствующих запросу.</returns>
        public async Task<List<CategoryDTO>> GetCategoriesAsync(GetListRequest req)
            => await WebClient.GetAsync<List<CategoryDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}"
            ) ?? [];

        /// <summary>
        /// Получить категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>DTO категории с указанным идентификатором.</returns>
        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
            => await WebClient.GetAsync<CategoryDTO>($"{_base}/{id}");

        /// <summary>
        /// Создать новую категорию.
        /// </summary>
        /// <param name="dto">Данные для создания категории.</param>
        /// <returns>DTO созданной категории (включая присвоенный идентификатор и прочие заполненные поля).</returns>
        public async Task<CategoryDTO> CreateCategoryAsync(CategoryWriteDTO dto)
            => await WebClient.PostAsync<CategoryDTO>(_base, dto);

        /// <summary>
        /// Обновить существующую категорию.
        /// </summary>
        /// <param name="id">Идентификатор категории для обновления.</param>
        /// <param name="dto">Данные для обновления категории.</param>
        /// <returns>Задача, представляющая асинхронную операцию обновления.</returns>
        public async Task UpdateCategoryAsync(int id, CategoryWriteDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}", dto);

        /// <summary>
        /// Удалить категорию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории для удаления.</param>
        /// <returns>Задача, представляющая асинхронную операцию удаления.</returns>
        public async Task DeleteCategoryAsync(int id)
            => await WebClient.DeleteAsync<object?>($"{_base}/{id}");

        /// <summary>
        /// Получить общее количество категорий.
        /// </summary>
        /// <returns>Общее количество записей категорий в системе.</returns>
        public async Task<int> GetCategoriesCountAsync()
            => await WebClient.GetAsync<int>($"{_base}/count");
    }
}