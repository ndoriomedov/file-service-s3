namespace WebService.Models.Dto.Responses
{
  public class FileResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long Size { get; set; }
    public string Bucket { get; set; }
  }
}
