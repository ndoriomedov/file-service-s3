using Microsoft.AspNetCore.Mvc;

namespace WebService.Business.Interfaces
{
  public interface IGetFileCommand
  {
    public Task<(MemoryStream stream, string fileName, string fileContentType)> ExecuteAsync(Guid id);
  }
}
