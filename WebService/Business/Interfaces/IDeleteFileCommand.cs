namespace WebService.Business.Interfaces
{
  public interface IDeleteFileCommand
  {
    Task ExecuteAsync(Guid fileId);
  }
}
