using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Shared.WebService
{
    public static class RequestExtensions
    { 
        /// <summary>
        /// Получение строки запроса из объекта анонимного типа
        /// </summary>
        public static string DataString(this object objectData)
        {
            var stringBuilder = new StringBuilder();
            objectData.GetType().GetProperties().ToList().ForEach(w => WSRequestEncode.AddParametr(stringBuilder, w?.Name, w?.GetValue(objectData)?.ToString()));

            return stringBuilder.ToString();
        }
        /// <summary>
        /// Получение времени для запроса
        /// </summary>
        public static string DateRequestString(this DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss", new CultureInfo("ru-RU"));
        }
    }
}
