using SendGrid;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Emails
{
    public interface IEmailsService
    {
        Task<Response> SendAsync(string to, string subject, string content);
    }
}
