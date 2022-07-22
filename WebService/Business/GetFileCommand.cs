using Microsoft.AspNetCore.Mvc;
using WebService.Business.Interfaces;
using WebService.Minio.Interfaces;
using WebService.Models.Db;
using WebService.Sql.Provider.Repositories.Interfaces;

namespace WebService.Business
{
  public class GetFileCommand : IGetFileCommand
  {
    private readonly IMinioHelper _minioHelper;
    private readonly IFileRepository _fileRepository;

    public GetFileCommand(
      IMinioHelper minioHelper,
      IFileRepository fileRepository)
    {
      _minioHelper = minioHelper;
      _fileRepository = fileRepository;
    }

    public async Task<(MemoryStream stream, string fileName, string fileContentType)> ExecuteAsync(Guid fileId)
    {
      MemoryStream stream = new MemoryStream();

      DbFile? downloadedFile = await _fileRepository.GetAsync(fileId);

      if (downloadedFile is null || !await _minioHelper.GetFileAsync(downloadedFile.Name, stream))
      {
        return default;
      }

      return (stream, downloadedFile.Name, downloadedFile.ContentType);
    }
  }
}
