using Clients.helpers;
using Clients.webClients.tags.interfaces;
using Shared.dtos.tags;
using Shared.requests;

namespace Clients.webClients.tags.clients
{
    /// <summary>
    /// Веб-клиент для работы с ресурсом тегов на сервере.
    /// </summary>
    /// <remarks>
    /// Этот класс формирует HTTP-запросы к конечным точкам, связанным с сущностью "тег".
    /// Все методы асинхронные и возвращают результаты вызова через общий статический <c>WebClient</c>.
    /// Базовый путь для запросов: <c>/tags</c>.
    /// </remarks>
    public class TagsWebClient : ITagsWebClient
    {
        private const string _base = "/tags";

        /// <summary>
        /// Получить список тегов с параметрами фильтрации/пагинации.
        /// </summary>
        /// <param name="req">Параметры запроса (фильтрация, сортировка, страница и т.д.).</param>
        /// <returns>Список DTO тегов, соответствующих запросу.</returns>
        /// <remarks>
        /// Формируется GET-запрос на <c>/tags{query}</c>, где <c>{query}</c> — результат <c>QueryBuilder.ToQueryString(req)</c>.
        /// Бросает исключение, если HTTP-запрос завершился неудачей.
        /// </remarks>
        public async Task<List<TagDTO>> GetTagsAsync(GetListRequest req)
            => await WebClient.GetAsync<List<TagDTO>>(
                $"{_base}{QueryBuilder.ToQueryString(req)}"
            ) ?? [];

        /// <summary>
        /// Получить тег по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор тега.</param>
        /// <returns>DTO тега с указанным идентификатором.</returns>
        /// <remarks>
        /// Выполняет GET-запрос на <c>/tags/{id}</c>.
        /// Если тег не найден, поведение зависит от реализации <c>WebClient.GetAsync</c> (может быть выброшено исключение или возвращён null).
        /// </remarks>
        public async Task<TagDTO> GetTagByIdAsync(int id)
            => await WebClient.GetAsync<TagDTO>($"{_base}/{id}");

        /// <summary>
        /// Создать новый тег.
        /// </summary>
        /// <param name="dto">Данные тега для создания.</param>
        /// <returns>DTO созданного тега (обычно содержит новый идентификатор и прочие заполненные полями значения).</returns>
        /// <remarks>
        /// Выполняет POST-запрос на <c>/tags</c> с телом запроса <c>dto</c>.
        /// </remarks>
        public async Task<TagDTO> CreateTagAsync(TagWriteDTO dto)
            => await WebClient.PostAsync<TagDTO>(_base, dto);

        /// <summary>
        /// Обновить существующий тег.
        /// </summary>
        /// <param name="id">Идентификатор тега для обновления.</param>
        /// <param name="dto">Набор полей для обновления.</param>
        /// <returns>Асинхронная задача. При успешном выполнении обновление прошло успешно.</returns>
        /// <remarks>
        /// Выполняет PUT-запрос на <c>/tags/{id}</c> с телом запроса <c>dto</c>.
        /// Возвращаемое значение сервера игнорируется (используется <c>object?</c>).
        /// </remarks>
        public async Task UpdateTagAsync(int id, TagWriteDTO dto)
            => await WebClient.PutAsync<object?>($"{_base}/{id}", dto);

        /// <summary>
        /// Удалить тег по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого тега.</param>
        /// <returns>Асинхронная задача. При успешном выполнении тег удалён.</returns>
        /// <remarks>
        /// Выполняет DELETE-запрос на <c>/tags/{id}</c>.
        /// </remarks>
        public async Task DeleteTagAsync(int id)
            => await WebClient.DeleteAsync<object?>($"{_base}/{id}");

        /// <summary>
        /// Получить общее количество тегов.
        /// </summary>
        /// <returns>Количество всех тегов на сервере.</returns>
        /// <remarks>
        /// Выполняет GET-запрос на <c>/tags/count</c>.
        /// </remarks>
        public async Task<int> GetTagsCountAsync()
            => await WebClient.GetAsync<int>($"{_base}/count");
    }
}