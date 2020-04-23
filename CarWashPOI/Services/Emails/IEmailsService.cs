using SendGrid;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Emails
{
    public interface IEmailsService
    {
        Task<Response> Send(string to, string subject, string content);
    }
}
