using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Users
{
    public interface IUsersService
    {
        Task<int> GetTotalUsersAsync();
    }
}
