using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Emails
{
    public class SendGridEmailsService : IEmailsService
    {
        private readonly IConfiguration configuration;

        public SendGridEmailsService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Response> SendAsync(string to, string subject, string content)
        {
            SendGridClient client = new SendGridClient(configuration["SendGrid:ApiKey"]);
            EmailAddress from = new EmailAddress(configuration["SendGrid:From"], configuration["SendGrid:Name"]);
            EmailAddress recipient = new EmailAddress(to);
            SendGridMessage message = MailHelper.CreateSingleEmail(from, recipient, subject, content, content);
            Response response = await client.SendEmailAsync(message);

            return response;
        }
    }
}