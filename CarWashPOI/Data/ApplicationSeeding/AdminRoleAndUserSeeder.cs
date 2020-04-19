using CarWashPOI.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CarWashPOI.Data.ApplicationSeeding
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
            IdentityRole adminRole = roleManager.FindByNameAsync(configuration["AdminUser:AdminRoleName"]).GetAwaiter().GetResult();

            if (adminRole == null)
            {
                adminRole = new IdentityRole(configuration["AdminUser:AdminRoleName"]);
                roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
            }
        }

        public void CreateAdminUser()
        {
            ApplicationUser adminUser = userManager.FindByNameAsync(configuration["AdminUser:Username"]).GetAwaiter().GetResult();

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = configuration["AdminUser:Username"],
                    Email = configuration["AdminUser:Email"]
                };
                userManager.CreateAsync(adminUser, configuration["AdminUser:Password"]).GetAwaiter().GetResult(); ;

                userManager.AddToRoleAsync(adminUser, configuration["AdminUser:AdminRoleName"]).GetAwaiter().GetResult();
            }
        }
    }
}
