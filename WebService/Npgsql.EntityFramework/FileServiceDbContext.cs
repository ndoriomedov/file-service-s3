using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebService.Models.Db;
using WebService.Sql.Provider;

namespace WebService.Npgsql.EntityFramework
{
  public class FileServiceDbContext : DbContext, IDataProvider
  {
    public DbSet<DbFile> Files { get; set; }

    public FileServiceDbContext(DbContextOptions<FileServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    void IDataProvider.Save()
    {
      SaveChanges();
    }

    async Task IDataProvider.SaveAsync()
    {
      await SaveChangesAsync();
    }

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;

      return Entry(obj).State;
    }

    public void EnsureDeleted()
    {
      Database.EnsureDeleted();
    }
  }
}
