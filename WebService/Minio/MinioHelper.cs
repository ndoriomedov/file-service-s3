using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using WebService.Extensions;
using WebService.Minio.Interfaces;
using WebService.Models.Configurations;

namespace WebService.Minio
{
  public class MinioHelper : IMinioHelper
  {
    private readonly string _defaultBucket;
    private readonly MinioClient _minioClient;
    
    private readonly ILogger<MinioHelper> _logger;

    public MinioHelper(
      IOptions<MinioConfiguration> minioConfiguration,
      ILogger<MinioHelper> logger)
    {
      _logger = logger;

      string endpoint = Environment.GetEnvironmentVariable("MinioEndpoint").NullIfEmpty()
        ?? minioConfiguration.Value.Endpoint;
      string? accessKey = Environment.GetEnvironmentVariable("MinioAccessKey").NullIfEmpty()
        ?? minioConfiguration.Value.DevAccessKey;
      string? secretKey = Environment.GetEnvironmentVariable("MinioSecretKey").NullIfEmpty()
        ?? minioConfiguration.Value.DevSecretKey;

      _defaultBucket = minioConfiguration.Value.DefaultBucket.ToLower();

      _minioClient = new MinioClient()
        .WithEndpoint(endpoint)
        .WithCredentials(accessKey, secretKey)
        .Build();
    }

    public async Task<bool> GetFileAsync(string fileName, MemoryStream output, string? bucket = null, List<string>? errors = null)
    {
      bucket = bucket.NullIfEmpty()?.ToLower() ?? _defaultBucket;

      try
      {
        GetObjectArgs getArgs = new GetObjectArgs()
          .WithBucket(bucket)
          .WithObject(fileName)
          .WithCallbackStream((stream) => stream.CopyTo(output));

        await _minioClient.GetObjectAsync(getArgs);

        return true;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.Message);

        errors?.Add(ex.Message);

        return false;
      }
    }

    public async Task<bool> UploadFileAsync(IFormFile uploadedFile, string fileName, string? bucket = null, List<string>? errors = null)
    {
      bucket = bucket.NullIfEmpty()?.ToLower() ?? _defaultBucket;

      try
      {
        if (!await _minioClient.BucketExistsAsync(bucket))
        {
          await _minioClient.MakeBucketAsync(bucket);
        }

        PutObjectArgs putObjectArgs =
          new PutObjectArgs()
            .WithBucket(bucket)
            .WithObject(fileName)
            .WithContentType(uploadedFile.ContentType)
            .WithObjectSize(uploadedFile.Length)
            .WithStreamData(uploadedFile.OpenReadStream());

        await _minioClient.PutObjectAsync(putObjectArgs);

        return true;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex.ToString());

        errors?.Add(ex.Message);

        return false;
      }
    }

    public async Task<bool> RemoveFileAsync(string fileName, string? bucket = null, List<string>? errors = null)
    {
      bucket = bucket.NullIfEmpty()?.ToLower() ?? _defaultBucket;

      try
      {
        RemoveObjectArgs rmArgs = new RemoveObjectArgs()
          .WithBucket(bucket)
          .WithObject(fileName);

        await _minioClient.RemoveObjectAsync(rmArgs);

        return true;
      }
      catch(Exception ex)
      {
        _logger.LogError(ex.ToString());

        errors?.Add(ex.ToString());

        return false;
      }
    }
  }
}
