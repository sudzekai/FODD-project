using Shared.responses;
using Shared.types.exceptions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Clients.webClients
{
    /// <summary>
    /// Статический утилитарный клиент для выполнения HTTP-запросов к API.
    /// </summary>
    /// <remarks>
    /// Класс использует один статический экземпляр <see cref="HttpClient"/> для переиспользования соединений и снижения накладных расходов.
    /// Все методы поддерживают отмену через <see cref="CancellationToken"/> и возвращают десериализованный объект типа <typeparamref name="T"/> или <c>null</c>,
    /// если запрос завершился с ошибочным HTTP-статусом.
    /// </remarks>
    public static class WebClient
    {
        /// <summary>
        /// Общий <see cref="HttpClient"/>, используемый для всех запросов.
        /// Экземпляр переиспользуется и не должен уничножаться каждым вызовом.
        /// </summary>
        private static readonly HttpClient _httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        /// <summary>
        /// Устанавливает базовый адрес для всех последующих запросов.
        /// </summary>
        /// <param name="baseUrl">Базовый URL (например, <c>"https://api.example.com/"</c>).</param>
        /// <exception cref="ArgumentNullException">Если <paramref name="baseUrl"/> равен <c>null</c>.</exception>
        /// <exception cref="UriFormatException">Если <paramref name="baseUrl"/> не является корректным URI.</exception>
        /// <remarks>
        /// После установки базового адреса пути, передаваемые в методах <c>GetAsync</c>, <c>PostAsync</c> и т.д. интерпретируются относительно него.
        /// </remarks>
        public static void SetBaseUrl(string baseUrl)
        {
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        /// <summary>
        /// Добавляет или обновляет HTTP-заголовок по указанному ключу.
        /// </summary>
        /// <param name="key">Имя заголовка.</param>
        /// <param name="value">Значение заголовка.</param>
        /// <remarks>
        /// Если заголовок с данным именем уже присутствует, он будет заменён новым значением.
        /// Подходит для установки кастомных заголовков (например, идентификаторов кореляции).
        /// </remarks>
        public static void AddHeader(string key, string value)
        {
            if (_httpClient.DefaultRequestHeaders.Contains(key))
                _httpClient.DefaultRequestHeaders.Remove(key);

            _httpClient.DefaultRequestHeaders.Add(key, value);
        }

        /// <summary>
        /// Устанавливает в заголовках JWT Bearer токен аутентификации.
        /// </summary>
        /// <param name="token">JWT токен без префикса <c>"Bearer "</c>.</param>
        /// <remarks>
        /// Если заголовок Authorization уже присутствует, он будет заменён.
        /// </remarks>
        public static void AddBearerToken(string token)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
                _httpClient.DefaultRequestHeaders.Remove("Authorization");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        /// <summary>
        /// Выполняет HTTP GET запрос и десериализует JSON-ответ в тип <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Ожидаемый тип результата.</typeparam>
        /// <param name="url">Относительный или абсолютный URL запроса.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Десериализованный объект типа <typeparamref name="T"/>, или <c>null</c> при ошибочном статусе ответа.</returns>
        public static Task<T?> GetAsync<T>(string url, CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Get, url, null, ct);

        /// <summary>
        /// Выполняет HTTP DELETE запрос и десериализует JSON-ответ в тип <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Ожидаемый тип результата.</typeparam>
        /// <param name="url">Относительный или абсолютный URL запроса.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Десериализованный объект типа <typeparamref name="T"/>, или <c>null</c> при ошибочном статусе ответа.</returns>
        public static Task<T?> DeleteAsync<T>(string url, CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Delete, url, null, ct);

        /// <summary>
        /// Выполняет HTTP POST запрос с сериализуемым телом и десериализует JSON-ответ в тип <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Ожидаемый тип результата.</typeparam>
        /// <param name="url">Относительный или абсолютный URL запроса.</param>
        /// <param name="body">Объект, который будет сериализован в JSON и отправлен в теле запроса. Может быть <c>null</c>.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Десериализованный объект типа <typeparamref name="T"/>, или <c>null</c> при ошибочном статусе ответа.</returns>
        public static Task<T?> PostAsync<T>(string url, object? body, CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Post, url, body, ct);

        /// <summary>
        /// Выполняет HTTP PUT запрос с сериализуемым телом и десериализует JSON-ответ в тип <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Ожидаемый тип результата.</typeparam>
        /// <param name="url">Относительный или абсолютный URL запроса.</param>
        /// <param name="body">Объект, который будет сериализован в JSON и отправлен в теле запроса. Может быть <c>null</c>.</param>
        /// <param name="ct">Токен отмены запроса.</param>
        /// <returns>Десериализованный объект типа <typeparamref name="T"/>, или <c>null</c> при ошибочном статусе ответа.</returns>
        public static Task<T?> PutAsync<T>(string url, object? body, CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Put, url, body, ct);

        /// <summary>
        /// Внутренний метод-исполнитель HTTP-запроса: сериализует тело (если указано), отправляет запрос
        /// и десериализует JSON-ответ в оболочку <see cref="ResponseEnvelope{T}"/>, после чего возвращает поле <c>Data</c>.
        /// </summary>
        /// <typeparam name="T">Ожидаемый тип полезных данных в поле <c>Data</c> ответа.</typeparam>
        /// <param name="method">HTTP-метод (GET, POST, PUT, DELETE и т.д.).</param>
        /// <param name="url">Относительный или абсолютный URL запроса. Если задан <see cref="_httpClient.BaseAddress"/>, может быть относительным.</param>
        /// <param name="body">Тело запроса. Если не <c>null</c>, будет сериализовано в JSON и установлено как содержимое запроса с типом "application/json".</param>
        /// <param name="ct">Токен отмены для прерывания операции.</param>
        /// <returns>Десериализованный объект типа <typeparamref name="T"/>, извлечённый из <c>ResponseEnvelope.Data</c>.</returns>
        /// <exception cref="ApiException">
        /// Выбрасывается в следующих случаях:
        /// - если ответ не удалось распарсить как ожидаемый формат (<c>ResponseEnvelope{T}</c>) — сообщение "Неверный формат ответа сервера";
        /// - если сервер вернул ошибочный HTTP-статус — в сообщении используется <c>envelope.Error?.Message</c> или "Ошибка сервера";
        /// - если тело ответа пустое или в оболочке указано, что операция неуспешна — используется <c>envelope.Error?.Message</c> или "Ошибка сервера".
        /// </exception>
        /// <exception cref="System.Net.Http.HttpRequestException">Могут быть проброшены исключения, связанные с сетевыми ошибками при выполнении запроса.</exception>
        /// <exception cref="System.Threading.Tasks.TaskCanceledException">Может быть выброшено при таймауте или отмене через <paramref name="ct"/>.</exception>
        /// <remarks>
        /// - Метод ожидает, что сервер возвращает JSON в формате <see cref="ResponseEnvelope{T}">ResponseEnvelope</see>:
        ///   <code>
        ///   { "isSuccess": true|false, "data": {...}, "error": { "message": "...", "code": "..." } }
        ///   </code>
        /// - Сериализация тела запроса выполняется стандартными средствами <see cref="System.Text.Json"/> через <see cref="JsonContent.Create(object?)"/>.
        ///   При необходимости тонкой настройки сериализации (например, конвертеров или опций) следует заменить механизм формирования контента.
        /// - При ошибочных HTTP-статусах (<c>2xx</c> считается успешным) метод попытается извлечь сообщение ошибки из оболочки ответа и вернуть его в виде <see cref="ApiException"/>.
        /// - Метод использует общий статический экземпляр <see cref="_httpClient"/>, поэтому заголовки по умолчанию и базовый адрес влияют на все вызовы.
        /// </remarks>
        private static async Task<T> SendAsync<T>(
            HttpMethod method,
            string url,
            object? body,
            CancellationToken ct)
        {
            using var request = new HttpRequestMessage(method, url);

            if (body != null)
            {
                request.Content = JsonContent.Create(body);
            }

            using var response = await _httpClient.SendAsync(request, ct);

            ResponseEnvelope<T>? envelope;

            try
            {
                envelope = await response.Content.ReadFromJsonAsync<ResponseEnvelope<T>>(cancellationToken: ct);
            }
            catch
            {
                throw new ApiException("Неверный формат ответа сервера");
            }

            if (!response.IsSuccessStatusCode)
                throw new ApiException(envelope?.Error?.Message ?? "Ошибка сервера",
                                        envelope?.Error?.Code);

            if (envelope is null)
                throw new ApiException("Пустой ответ сервера");

            if (!envelope.IsSuccess)
                throw new ApiException(envelope.Error?.Message ?? "Ошибка сервера",
                                        envelope.Error?.Code);

            return envelope.Data!;
        }
    }
}