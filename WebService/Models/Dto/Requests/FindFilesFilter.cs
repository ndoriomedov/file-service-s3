using Microsoft.AspNetCore.Mvc;

namespace WebService.Models.Dto.Requests
{
  public class FindFilesFilter
  {
    [FromQuery(Name = "Name")]
    public string? Name { get; set; } = null;

    [FromQuery(Name = "Bucket")]
    public string? Bucket { get; set; } = null;
  }
}
