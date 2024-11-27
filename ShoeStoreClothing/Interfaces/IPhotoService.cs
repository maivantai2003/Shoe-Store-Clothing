using CloudinaryDotNet.Actions;

namespace ShoeStoreClothing.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAysnc(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicUrl);

    }
}
