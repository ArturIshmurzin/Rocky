using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Rocky_Utility
{
    /// <summary>
    /// Класс для отправки сообщений
    /// </summary>
    public class MailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// настройки мэйлджета
        /// </summary>
        public MailJetSettings MailJetSettings { get; set; }

        public MailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        private async Task Execute(string email, string subject, string body)
        {
            MailJetSettings = _configuration.GetSection("MailJet").Get<MailJetSettings>();

            MailjetClient client = new MailjetClient(MailJetSettings.ApiKey, MailJetSettings.SecretKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
             .Property(Send.Messages, new JArray {
            new JObject {
      {
       "From",
       new JObject {
        {"Email", "artur.ishmurzin@protonmail.com"},
        {"Name", "Артур"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "Артур"
         }
        }
       }
      }, {
       "Subject",
       subject
      },  {
       "HTMLPart",
       body
      },
     }
             });
            var result = await client.PostAsync(request);
        }
    }
}
