using Clients.helpers;
using Clients.webClients.suppliers.interfaces;
using Shared.dtos.suppliers;
using Shared.requests;

namespace Clients.webClients.suppliers.clients
{
    /// <summary>
    /// HTTP-клиент для работы с API поставщиков.
    /// </summary>
    /// <remarks>
    /// Методы выполняют асинхронные HTTP-вызовы через статический `WebClient` из `Clients.helpers`.
    /// Исключения, связанные с сетью или ответом сервера, пробрасываются далее и должны обрабатываться вызывающей стороной.
    /// </remarks>
    public class SuppliersWebClient : ISuppliersWebClient
    {
        private const string _base = "/suppliers";

        /// <summary>
        /// Асинхронно получает список поставщиков с учётом параметров запроса.
        /// </summary>
        /// <param name="req">Параметры пагинации, фильтрации и сортировки (модель <see cref="GetListRequest"/>).</param>
        /// <returns>
        /// Список DTO поставщиков (<see cref="SupplierDTO"/>). 
        /// Если API возвращает null, метод возвращает пустой список.
        /// </returns>
        public async Task<List<SupplierDTO>> GetSuppliersAsync(GetListRequest req)
            => await WebClient.GetAsync<List<SupplierDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}"
            ) ?? [];

        /// <summary>
        /// Асинхронно получает поставщика по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор поставщика.</param>
        /// <returns>
        /// DTO поставщика (<see cref="SupplierDTO"/>).
        /// В случае отсутствия поставщика поведение зависит от API (может быть возвращён null или выброшено исключение).
        /// </returns>
        public async Task<SupplierDTO> GetSupplierByIdAsync(int id)
            => await WebClient.GetAsync<SupplierDTO>($"{_base}/{id}");

        /// <summary>
        /// Создаёт нового поставщика.
        /// </summary>
        /// <param name="dto">Данные для создания поставщика (<see cref="SupplierWriteDTO"/>).</param>
        /// <returns>DTO созданного поставщика, как возвращено сервером.</returns>
        public async Task<SupplierDTO> CreateSupplierAsync(SupplierWriteDTO dto)
            => await WebClient.PostAsync<SupplierDTO>(_base, dto);

        /// <summary>
        /// Обновляет данные поставщика по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор обновляемого поставщика.</param>
        /// <param name="dto">Новые данные поставщика (<see cref="SupplierWriteDTO"/>).</param>
        /// <returns>Асинхронная задача. При ошибке серверного запроса будет выброшено исключение.</returns>
        public async Task UpdateSupplierAsync(int id, SupplierWriteDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}", dto);

        /// <summary>
        /// Удаляет поставщика по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор поставщика для удаления.</param>
        /// <returns>Асинхронная задача. При ошибке серверного запроса будет выброшено исключение.</returns>
        public async Task DeleteSupplierAsync(int id)
            => await WebClient.DeleteAsync<object?>($"{_base}/{id}");

        /// <summary>
        /// Получает общее количество поставщиков.
        /// </summary>
        /// <returns>Целочисленное значение — количество поставщиков в системе.</returns>
        public async Task<int> GetSuppliersCountAsync()
            => await WebClient.GetAsync<int>($"{_base}/count");
    }
}