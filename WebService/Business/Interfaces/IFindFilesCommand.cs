using WebService.Models.Dto.Requests;
using WebService.Models.Dto.Responses;

namespace WebService.Business.Interfaces
{
  public interface IFindFilesCommand
  {
    Task<List<Models.Dto.Responses.FileResponse>> ExecuteAsync(FindFilesFilter filter);
  }
}
