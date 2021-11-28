using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocky_Utility
{
    /// <summary>
    /// настройки мэйлджета
    /// </summary>
    public class MailJetSettings
    {
        /// <summary>
        /// Ключ апи
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// секретный ключ
        /// </summary>
        public string SecretKey { get; set; }
    }
}
