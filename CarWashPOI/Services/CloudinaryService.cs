using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;

namespace CarWashPOI.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Account cloudinaryAccount;

        public Cloudinary Cloudinary { get; }

        public CloudinaryService(IConfiguration configurtaion)
        {
            cloudinaryAccount = new Account(
                configurtaion["CloudinaryCredentials:CloudName"],
                configurtaion["CloudinaryCredentials:ApiKey"],
                configurtaion["CloudinaryCredentials:ApiSecret"]);

            Cloudinary = new Cloudinary(cloudinaryAccount);
        }
    }
}
