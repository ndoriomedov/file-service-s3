using Microsoft.Extensions.Options;
using WebService.Extensions;
using WebService.Mappers.Db.Interfaces;
using WebService.Models.Configurations;
using WebService.Models.Db;

namespace WebService.Mappers.Db
{
  public class DbFileMapper : IDbFileMapper
  {
    private readonly string _deafaultBucket;

    public DbFileMapper(
      IOptions<MinioConfiguration> minioConfiguration)
    {
      _deafaultBucket = minioConfiguration.Value.DefaultBucket;
    }

    public DbFile Map(IFormFile uploadedFile, string? bucket = null)
    {
      return new()
      {
        Id = Guid.NewGuid(),
        Name = Path.GetFileName(uploadedFile.FileName),
        ContentType = uploadedFile.ContentType,
        Size = uploadedFile.Length,
        Bucket = bucket?.ToLower().NullIfEmpty() ?? _deafaultBucket
      };
    }
  }
}
