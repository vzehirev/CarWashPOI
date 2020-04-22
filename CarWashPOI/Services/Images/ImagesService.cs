using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Images
{
    public class ImagesService : IImagesService
    {
        private const int maxImageWidth = 2100;
        private const int maxImageHeight = 2100;
        private readonly ICloudinaryService cloudinaryService;

        public ImagesService(ICloudinaryService cloudinaryService)
        {
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<string> UploadImageAsync(Stream imageFileStream)
        {
            ImageUploadParams imageUploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), imageFileStream),
                Folder = "CarWashPOI",
                Transformation = new Transformation().Width(maxImageWidth).Height(maxImageHeight).Crop("limit"),
            };

            ImageUploadResult imageUploadResult = await cloudinaryService.Cloudinary.UploadAsync(imageUploadParams);

            return imageUploadResult.SecureUri.OriginalString;
        }
    }
}
