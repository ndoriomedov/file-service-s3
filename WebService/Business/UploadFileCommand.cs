using System.Net;
using WebService.Business.Interfaces;
using WebService.Mappers.Db.Interfaces;
using WebService.Minio.Interfaces;
using WebService.Models.Db;
using WebService.Sql.Provider.Repositories.Interfaces;

namespace WebService.Business
{
  public class UploadFileCommand : IUploadFileCommand
  {
    private readonly IMinioHelper _minioHelper;
    private readonly IDbFileMapper _dbFileMapper;
    private readonly IFileRepository _fileRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UploadFileCommand(
      IMinioHelper minioHelper,
      IDbFileMapper dbFileMapper,
      IFileRepository fileRepository,
      IHttpContextAccessor httpContextAccessor)
    {
      _minioHelper = minioHelper;
      _dbFileMapper = dbFileMapper;
      _fileRepository = fileRepository;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid?> ExecuteAsync(IFormFile uploadedFile, string? bucket = null)
    {
      DbFile dbFile = _dbFileMapper.Map(uploadedFile, bucket);

      if (!await _minioHelper.UploadFileAsync(uploadedFile, dbFile.Name, dbFile.Bucket))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;
        return null;
      }

      await _fileRepository.CreateAsync(dbFile);

      return dbFile.Id;
    }
  }
}
