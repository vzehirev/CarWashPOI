using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashPOI.Services
{
    public interface IImagesService
    {
        public Task<string> UploadImageAsync(Stream imageFileStream);
    }
}
