using Microsoft.EntityFrameworkCore;
using WebService.Models.Db;

namespace WebService.Sql.Provider
{
  public interface IDataProvider
  {
    DbSet<DbFile> Files { get; set; }

    void Save();
    Task SaveAsync();
    object MakeEntityDetached(object obj);
    void EnsureDeleted();
  }
}
