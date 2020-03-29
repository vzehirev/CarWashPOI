using CarWashPOI.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CarWashPOI.Data
{
    public class AdminRoleAndUserSeeder
    {
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminRoleAndUserSeeder(
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public void CreateAdminRole()
        {
            IdentityRole adminRole = roleManager.FindByNameAsync("admin").GetAwaiter().GetResult();

            if (adminRole == null)
            {
                adminRole = new IdentityRole("admin");
                roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
            }
        }

        public void CreateAdminUser()
        {
            ApplicationUser adminUser = userManager.FindByNameAsync("admin").GetAwaiter().GetResult();

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = configuration["AdminUserCredentials:Username"],
                    Email = configuration["AdminUserCredentials:Email"]
                };
                userManager.CreateAsync(adminUser, configuration["AdminUserCredentials:Password"]).GetAwaiter().GetResult(); ;

                userManager.AddToRoleAsync(adminUser, "admin").GetAwaiter().GetResult();
            }
        }
    }
}
