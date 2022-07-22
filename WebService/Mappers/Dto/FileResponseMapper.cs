using WebService.Mappers.Dto.Interfaces;
using WebService.Models.Db;
using WebService.Models.Dto.Responses;

namespace WebService.Mappers.Dto
{
  public class FileResponseMapper : IFileResponseMapper
  {
    public FileResponse Map(DbFile file)
    {
      return new()
      {
        Id = file.Id,
        Name = file.Name,
        Size = file.Size,
        Bucket = file.Bucket,
      };
    }

    public List<FileResponse> Map(List<DbFile> files)
    {
      return files.Select(file => Map(file)).ToList();
    }
  }
}
