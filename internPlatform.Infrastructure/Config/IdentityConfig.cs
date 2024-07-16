using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
namespace internPlatform.Infrastructure.Config
{
    public class EmailService : IIdentityMessageService
    {
        private readonly string mailerSendApiKey = "mlsn.a52a043458519db22cd7e737ea31a9aeee14bd3b6090a5b3e82c5ea83fa4b084";
        private readonly HttpClient httpClient;

        public EmailService()
        {
            httpClient = new HttpClient();
            // Set the base address if all your requests will be to the same domain
            httpClient.BaseAddress = new Uri("https://api.mailersend.com/v1/");
            // Add the MailerSend API key to the request headers
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mailerSendApiKey);
        }
        public async Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            await SendEmailViaMailerSendAsync(message);
        }

        private async Task SendEmailViaMailerSendAsync(IdentityMessage message)
        {
            var emailData = new
            {
                from = new { email = "MS_noFwrb@trial-v69oxl5o97rg785k.mlsender.net", name = "Office" },
                to = new[] { new { email = message.Destination, name = "Recipient Name" } },
                subject = message.Subject,
                text = message.Body,
                html = $"<b>{message.Body}</b>",
                personalization = new[]
                {
                    new
                    {
                        email = message.Destination,
                        data = new { company = "Stefanini Group" }
                    }
                }
            };
            var jsonContent = JsonConvert.SerializeObject(emailData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("email", content);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error response
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Failed to send email: {errorMessage}");
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);

        }

    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    
}
