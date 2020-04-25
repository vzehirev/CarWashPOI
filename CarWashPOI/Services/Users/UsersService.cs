using CarWashPOI.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await dbContext.Users.CountAsync();
        }
    }
}
