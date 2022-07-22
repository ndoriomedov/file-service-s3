using WebService.Business.Interfaces;
using WebService.Mappers.Dto.Interfaces;
using WebService.Models.Dto.Requests;
using WebService.Models.Dto.Responses;
using WebService.Sql.Provider.Repositories.Interfaces;

namespace WebService.Business
{
  public class FindFilesCommand : IFindFilesCommand
  {
    private readonly IFileRepository _fileRepository;
    private readonly IFileResponseMapper _fileResponseMapper;

    public FindFilesCommand(
      IFileRepository fileRepository,
      IFileResponseMapper fileResponseMapper)
    {
      _fileRepository = fileRepository;
      _fileResponseMapper = fileResponseMapper;
    }

    public async Task<List<FileResponse>> ExecuteAsync(FindFilesFilter filter)
    {
      return _fileResponseMapper.Map(await _fileRepository.FindAsync(filter));
    }
  }
}
