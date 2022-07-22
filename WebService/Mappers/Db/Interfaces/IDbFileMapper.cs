using WebService.Models.Db;

namespace WebService.Mappers.Db.Interfaces
{
  public interface IDbFileMapper
  {
    DbFile Map(IFormFile uploadedFile, string? bucket = null);
  }
}
