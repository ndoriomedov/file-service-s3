using WebService.Models.Db;
using WebService.Models.Dto.Requests;

namespace WebService.Sql.Provider.Repositories.Interfaces
{
  public interface IFileRepository
  {
    Task CreateAsync(DbFile file);
    Task<DbFile?> GetAsync(Guid id);
    Task<List<DbFile>> FindAsync(FindFilesFilter filter);
    Task DeleteAsync(Guid id);
    Task DeleteAsync(DbFile deletedFile);
  }
}
