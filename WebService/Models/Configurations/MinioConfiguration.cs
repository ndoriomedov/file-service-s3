namespace WebService.Models.Configurations
{
  public class MinioConfiguration
  {
    public const string SectionName = "MinioConfiguration";

    public string Endpoint { get; set; }
    public string? DevAccessKey { get; set; }
    public string? DevSecretKey { get; set; }
    public string DefaultBucket { get; set; } = "Bucket";
  }
}
