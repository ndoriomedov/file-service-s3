using WebService.Models.Db;
using WebService.Models.Dto.Responses;

namespace WebService.Mappers.Dto.Interfaces
{
  public interface IFileResponseMapper
  {
    FileResponse Map(DbFile file);
    List<FileResponse> Map(List<DbFile> files);
  }
}
