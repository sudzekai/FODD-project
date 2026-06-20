using Clients.helpers;
using Clients.webClients.manufacturers.interfaces;
using Shared.dtos.manufacturers;
using Shared.requests;

namespace Clients.webClients.manufacturers.clients
{
    /// <summary>
    /// Веб‑клиент для работы с API производителей.
    /// Инкапсулирует HTTP-вызовы к маршруту <c>/manufacturers</c>.
    /// </summary>
    public class ManufacturersWebClient : IManufacturersWebClient
    {
        private const string _base = "/manufacturers";

        /// <summary>
        /// Получить список производителей с параметрами фильтрации/страничности.
        /// </summary>
        /// <param name="req">Параметры запроса списка (фильтры, сортировка, постраничность).</param>
        /// <returns>Список DTO производителей, соответствующих запросу.</returns>
        public async Task<List<ManufacturerDTO>> GetManufacturersAsync(GetListRequest req)
            => await WebClient.GetAsync<List<ManufacturerDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}"
            );

        /// <summary>
        /// Получить производителя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор производителя.</param>
        /// <returns>DTO производителя с указанным идентификатором.</returns>
        public async Task<ManufacturerDTO> GetManufacturerByIdAsync(int id)
            => await WebClient.GetAsync<ManufacturerDTO>($"{_base}/{id}");

        /// <summary>
        /// Создать нового производителя.
        /// </summary>
        /// <param name="dto">Данные для создания производителя.</param>
        /// <returns>DTO созданного производителя (включая присвоенный идентификатор и прочие заполненные поля).</returns>
        public async Task<ManufacturerDTO> CreateManufacturerAsync(ManufacturerWriteDTO dto)
            => await WebClient.PostAsync<ManufacturerDTO>(_base, dto);

        /// <summary>
        /// Обновить существующего производителя.
        /// </summary>
        /// <param name="id">Идентификатор производителя для обновления.</param>
        /// <param name="dto">Данные для обновления производителя.</param>
        /// <returns>Задача, представляющая асинхронную операцию обновления.</returns>
        public async Task UpdateManufacturerAsync(int id, ManufacturerWriteDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}", dto);

        /// <summary>
        /// Удалить производителя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор производителя для удаления.</param>
        /// <returns>Задача, представляющая асинхронную операцию удаления.</returns>
        public async Task DeleteManufacturerAsync(int id)
            => await WebClient.DeleteAsync<object?>($"{_base}/{id}");

        /// <summary>
        /// Получить общее количество производителей.
        /// </summary>
        /// <returns>Общее количество записей производителей в системе.</returns>
        public async Task<int> GetManufacturersCountAsync()
            => await WebClient.GetAsync<int>($"{_base}/count");
    }
}