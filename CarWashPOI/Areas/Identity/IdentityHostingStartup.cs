using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CarWashPOI.Areas.Identity.IdentityHostingStartup))]
namespace CarWashPOI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}