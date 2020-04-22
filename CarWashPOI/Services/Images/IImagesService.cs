using System.IO;
using System.Threading.Tasks;

namespace CarWashPOI.Services.Images
{
    public interface IImagesService
    {
        public Task<string> UploadImageAsync(Stream imageFileStream);
    }
}
