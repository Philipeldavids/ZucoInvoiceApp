using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadFileAsync(IFormFile file);
    }
}