using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rocky_Utility
{
    /// <summary>
    /// Класс с методами расширения для сессий
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Установка значения
        /// </summary>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        /// <summary>
        /// Получить значение
        /// </summary>
        public static T Get<T>(this ISession session, string key)
        {
            string stringValue = session.GetString(key);
            return stringValue == null ? default : JsonSerializer.Deserialize<T>(stringValue);
        }
    }
}
