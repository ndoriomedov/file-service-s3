using System.Net;
using WebService.Business.Interfaces;
using WebService.Minio.Interfaces;
using WebService.Models.Db;
using WebService.Sql.Provider.Repositories.Interfaces;

namespace WebService.Business
{
  public class DeleteFileCommand : IDeleteFileCommand
  {
    private readonly IMinioHelper _minioHelper;
    private readonly IFileRepository _fileRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteFileCommand(
      IMinioHelper minioHelper,
      IFileRepository fileRepository,
      IHttpContextAccessor httpContextAccessor)
    {
      _minioHelper = minioHelper;
      _fileRepository = fileRepository;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task ExecuteAsync(Guid fileId)
    {
      DbFile? file = await _fileRepository.GetAsync(fileId);

      if (file is null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

        return;
      }

      if (!await _minioHelper.RemoveFileAsync(file.Name, file.Bucket))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;

        return;
      }

      await _fileRepository.DeleteAsync(file);

      return;
    }
  }
}
