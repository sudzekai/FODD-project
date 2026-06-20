using System.Reflection;
using System.Text;
using System.Web;

namespace Clients.helpers
{
    /// <summary>
    /// Вспомогательный класс для формирования строки запроса (query string) из объекта.
    /// </summary>
    /// <remarks>
    /// Преобразует публичные экземплярные свойства объекта в пары "ключ=значение".
    /// Свойства со значением <c>null</c> или пустой/пробельной строкой пропускаются.
    /// Имена свойств и значения кодируются с помощью <see cref="HttpUtility.UrlEncode(string)"/>.
    /// Возвращает пустую строку, если `obj` равен <c>null</c> или если нет валидных свойств.
    /// </remarks>
    public static class QueryBuilder
    {
        /// <summary>
        /// Преобразует объект в строку запроса.
        /// </summary>
        /// <param name="obj">Объект, чьи публичные свойства будут преобразованы в параметры запроса.</param>
        /// <returns>
        /// Строка вида <c>?key1=value1&key2=value2</c> или пустая строка, если параметров нет.
        /// </returns>
        public static string ToQueryString(object obj)
        {
            if (obj == null) return string.Empty;

            var properties = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var sb = new StringBuilder();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);
                if (value == null) continue;

                var stringValue = value.ToString();
                if (string.IsNullOrWhiteSpace(stringValue)) continue;

                if (sb.Length > 0)
                    sb.Append('&');

                sb.Append(HttpUtility.UrlEncode(prop.Name));
                sb.Append('=');
                sb.Append(HttpUtility.UrlEncode(stringValue));
            }

            return sb.Length > 0 ? "?" + sb.ToString() : "";
        }
    }
}