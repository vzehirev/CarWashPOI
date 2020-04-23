using AutoMapper;
using CarWashPOI.Data;
using CarWashPOI.Data.ApplicationSeeding;
using CarWashPOI.Data.Models;
using CarWashPOI.Services.Articles;
using CarWashPOI.Services.Comments;
using CarWashPOI.Services.Emails;
using CarWashPOI.Services.Images;
using CarWashPOI.Services.Locations;
using CarWashPOI.Services.LocationTypes;
using CarWashPOI.Services.Towns;
using CarWashPOI.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CarWashPOI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // Add AutoMapper
            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperMappings());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            // Application seeders
            services.AddTransient<AdminRoleAndUserSeeder>();
            services.AddTransient<TownsCoordinatesSeeding>();

            // Application services
            services.AddTransient<ILocationsService, LocationsService>();
            services.AddTransient<ILocationTypesService, LocationTypesService>();
            services.AddTransient<ITownsService, TownsService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IArticlesService, ArticlesService>();
            services.AddTransient<IImagesService, CloudinaryImagesService>();
            services.AddTransient<IEmailsService, SendGridEmailsService>();
            services.AddTransient<IUsersService, UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ApplicationDbContext dbContext,
            AdminRoleAndUserSeeder adminRoleAndUserSeeder,
            TownsCoordinatesSeeding townsCoordinatesSeeding)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Administration",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            dbContext.Database.Migrate();

            adminRoleAndUserSeeder.CreateAdminRole();
            adminRoleAndUserSeeder.CreateAdminUser();
            townsCoordinatesSeeding.SeedTownsCoordinates();
        }
    }
}
