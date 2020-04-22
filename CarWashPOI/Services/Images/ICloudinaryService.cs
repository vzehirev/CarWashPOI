using CloudinaryDotNet;

namespace CarWashPOI.Services.Images
{
    public interface ICloudinaryService
    {
        public Cloudinary Cloudinary { get; }
    }
}
