using Microsoft.AspNetCore.Mvc;
using WebService.Business.Interfaces;
using WebService.Models.Dto.Requests;
using WebService.Models.Dto.Responses;

namespace WebService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class FileController : ControllerBase
  {
    [HttpPost]
    [DisableRequestSizeLimit]
    public async Task<Guid?> UploadFileAsync(
      [FromForm] IFormFile file,
      [FromQuery] string? bucket,
      [FromServices] IUploadFileCommand command)
    {
      return await command.ExecuteAsync(file, bucket);
    }

    [HttpGet]
    public async Task<ActionResult> GetFileAsync(
      [FromQuery] Guid fileId,
      [FromServices] IGetFileCommand command)
    {
      (MemoryStream stream, string fileName, string fileContentType) file = await command.ExecuteAsync(fileId);

      if (file == default)
      {
        return new NotFoundResult();
      }

      return File(
        fileContents: file.stream.ToArray(),
        contentType: file.fileContentType,
        fileDownloadName: file.fileName);
    }

    [HttpGet("find")]
    public async Task<List<FileResponse>> FindFilesAsync(
      [FromQuery] FindFilesFilter filter,
      [FromServices] IFindFilesCommand command)
    {
      return await command.ExecuteAsync(filter);
    }

    [HttpDelete]
    public async Task DeleteFileAsync(
      [FromQuery] Guid fileId,
      [FromServices] IDeleteFileCommand command)
    {
      await command.ExecuteAsync(fileId);
    }
  }
}
