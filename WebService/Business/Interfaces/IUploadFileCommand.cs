namespace WebService.Business.Interfaces
{
  public interface IUploadFileCommand
  {
    Task<Guid?> ExecuteAsync(IFormFile uploadedFile, string? bucket = null);
  }
}
