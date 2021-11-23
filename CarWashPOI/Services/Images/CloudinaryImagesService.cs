using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Images
{
    public class CloudinaryImagesService : IImagesService
    {
        private const int maxImageWidth = 2100;
        private const int maxImageHeight = 2100;
        private readonly IConfiguration configuration;
        private readonly Account cloudinaryAccount;
        private readonly Cloudinary cloudinary;

        public CloudinaryImagesService(IConfiguration configuration)
        {
            this.configuration = configuration;

            cloudinaryAccount = new Account(
                this.configuration["CloudinaryCredentials:CloudName"],
                this.configuration["CloudinaryCredentials:ApiKey"],
                this.configuration["CloudinaryCredentials:ApiSecret"]);

            cloudinary = new Cloudinary(cloudinaryAccount);
        }

        public async Task<string> UploadImageAsync(Stream imageFileStream)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), imageFileStream),
                Folder = configuration["CloudinaryCredentials:UploadFolder"],
                Transformation = new Transformation()
                .Width(maxImageWidth)
                .Height(maxImageHeight)
                .Crop(configuration["CloudinaryCredentials:CropType"]),
            };

            ImageUploadResult imageUploadResult = await cloudinary.UploadAsync(imageUploadParams);

            return imageUploadResult.SecureUrl.OriginalString;
        }
    }
}
