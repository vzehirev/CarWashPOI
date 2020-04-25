using System.Threading.Tasks;

namespace CarWashPOI.Services.Users
{
    public interface IUsersService
    {
        Task<int> GetTotalUsersAsync();
    }
}
