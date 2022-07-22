using Minio.DataModel;

namespace WebService.Minio.Interfaces
{
  public interface IMinioHelper
  {
    Task<bool> GetFileAsync(string fileName, MemoryStream output, string? bucket = null, List<string>? errors = null);
    Task<bool> UploadFileAsync(IFormFile uploadedFile, string fileName, string? bucket = null, List<string>? errors = null);
    Task<bool> RemoveFileAsync(string fileName, string? bucket = null, List<string>? errors = null);
  }
}
