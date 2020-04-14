using System.IO;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface IImagesService
    {
        public Task<string> UploadImageAsync(Stream imageFileStream);
    }
}
