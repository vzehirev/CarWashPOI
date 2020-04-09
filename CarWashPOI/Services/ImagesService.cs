using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public class ImagesService : IImagesService
    {
        private const int maxImageWidth = 900;
        private const int maxImageHeight = 900;
        private readonly ICloudinaryService cloudinaryService;

        public ImagesService(ICloudinaryService cloudinaryService)
        {
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<string> UploadImageAsync(Stream imageFileStream)
        {
            var imageUploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), imageFileStream),
                Folder = "WashLocations",
                Transformation = new Transformation().Width(maxImageWidth).Height(maxImageHeight).Crop("limit"),
            };

            var imageUploadResult = await cloudinaryService.Cloudinary.UploadAsync(imageUploadParams);

            return imageUploadResult.SecureUri.OriginalString;
        }
    }
}
