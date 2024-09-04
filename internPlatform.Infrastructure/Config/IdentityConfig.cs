using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace internPlatform.Infrastructure.Config
{
    public class EmailService : IIdentityMessageService
    {
        private readonly string mailerSendApiKey = "mlsn.426938e9a4f6e2d3ae1c4b33d64ea7c01c11f67bba1fcaba1ef9f28ee98c0938";
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
                from = new { email = "MS_noFwrb@trial-neqvygmyodzg0p7w.mlsender.net", name = "EventPlanner" },
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
