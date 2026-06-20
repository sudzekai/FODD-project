using Clients.webClients.statuses.interfaces;
using Shared.dtos.statuses;

namespace Clients.webClients.statuses.clients
{
    /// <summary>
    /// Клиент для взаимодействия с HTTP API раздела "Статусы".
    /// Выполняет CRUD-операции над сущностью статуса через общий вспомогательный
    /// класс `WebClient`.
    /// </summary>
    /// <remarks>
    /// Этот класс инкапсулирует маршруты API, относящиеся к статусам, и предоставляет
    /// асинхронные методы высокого уровня для получения, создания, обновления и удаления.
    /// Методы могут бросать сетевые исключения (например, <see cref="System.Net.Http.HttpRequestException"/>)
    /// или исключения отмены (<see cref="System.Threading.Tasks.TaskCanceledException"/>) при проблемах с сетью или таймаутах.
    /// </remarks>
    public class StatusesWebClient : IStatusesWebClient
    {
        private const string _base = "/statuses";

        /// <summary>
        /// Получить все доступные статусы.
        /// </summary>
        /// <returns>Список DTO статусов. Пустой список — если статусы отсутствуют.</returns>
        /// <remarks>Выполняет GET-запрос на маршрут <c>/statuses</c>.</remarks>
        public async Task<List<StatusDTO>> GetStatusesAsync()
            => await WebClient.GetAsync<List<StatusDTO>>(_base) ?? [];

        /// <summary>
        /// Получить статус по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор статуса.</param>
        /// <returns>DTO запрошенного статуса.</returns>
        /// <remarks>Выполняет GET-запрос на маршрут <c>/statuses/{id}</c>.
        /// Если статус не найден, API может вернуть соответствующий код состояния (например, 404).</remarks>
        public async Task<StatusDTO> GetStatusByIdAsync(int id)
            => await WebClient.GetAsync<StatusDTO>($"{_base}/{id}");

        /// <summary>
        /// Создать новый статус.
        /// </summary>
        /// <param name="dto">DTO для создания статуса.</param>
        /// <returns>DTO созданного статуса, как возвращено API (включая новый идентификатор).</returns>
        /// <remarks>Выполняет POST-запрос на маршрут <c>/statuses</c>.</remarks>
        public async Task<StatusDTO> CreateStatusAsync(StatusWriteDTO dto)
            => await WebClient.PostAsync<StatusDTO>(_base, dto);

        /// <summary>
        /// Обновить существующий статус.
        /// </summary>
        /// <param name="id">Идентификатор обновляемого статуса.</param>
        /// <param name="dto">DTO с обновлёнными значениями.</param>
        /// <returns>Асинхронная задача, завершение которой означает успешное выполнение запроса.</returns>
        /// <remarks>Выполняет PUT-запрос на маршрут <c>/statuses/{id}</c>. API может вернуть код состояния в зависимости от результата (например, 204 No Content при успешном обновлении).</remarks>
        public async Task UpdateStatusAsync(int id, StatusWriteDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}", dto);

        /// <summary>
        /// Удалить статус по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого статуса.</param>
        /// <returns>Асинхронная задача, завершение которой означает успешное выполнение запроса.</returns>
        /// <remarks>Выполняет DELETE-запрос на маршрут <c>/statuses/{id}</c>. При попытке удалить несуществующий ресурс API может вернуть 404.</remarks>
        public async Task DeleteStatusAsync(int id)
            => await WebClient.DeleteAsync<object?>($"{_base}/{id}");

        /// <summary>
        /// Получить общее количество статусов.
        /// </summary>
        /// <returns>Количество записей статусов (целое число).</returns>
        /// <remarks>Выполняет GET-запрос на маршрут <c>/statuses/count</c>.</remarks>
        public async Task<int> GetStatusesCountAsync()
            => await WebClient.GetAsync<int>($"{_base}/count");
    }
}