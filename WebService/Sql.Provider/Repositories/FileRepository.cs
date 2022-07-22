using Microsoft.EntityFrameworkCore;
using WebService.Models.Db;
using WebService.Models.Dto.Requests;
using WebService.Sql.Provider.Repositories.Interfaces;

namespace WebService.Sql.Provider.Repositories
{
  public class FileRepository : IFileRepository
  {
    private readonly IDataProvider _provider;

    public FileRepository(
      IDataProvider provider)
    {
      _provider = provider;
    }

    public Task CreateAsync(DbFile file)
    {
      _provider.Files.Add(file);

      return _provider.SaveAsync();
    }

    public Task<DbFile?> GetAsync(Guid id)
    {
      return _provider.Files.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<DbFile>> FindAsync(FindFilesFilter filter)
    {
      IQueryable<DbFile> filesQuery = _provider.Files.AsQueryable();

      if (!string.IsNullOrWhiteSpace(filter.Name))
      {
        filesQuery = filesQuery.Where(f => f.Name.ToLower().Contains(filter.Name.ToLower()));
      }

      if (!string.IsNullOrWhiteSpace(filter.Bucket))
      {
        filesQuery = filesQuery.Where(f => f.Bucket.ToLower().Contains(filter.Bucket.ToLower()));
      }

      return filesQuery.ToListAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
      DbFile? deletedFile = await GetAsync(id);

      if (deletedFile is null)
      {
        return;
      }

      _provider.Files.Remove(deletedFile);

      await _provider.SaveAsync();
    }

    public async Task DeleteAsync(DbFile deletedFile)
    {
      _provider.Files.Remove(deletedFile);

      await _provider.SaveAsync();
    }
  }
}
