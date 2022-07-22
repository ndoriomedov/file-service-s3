namespace WebService.Extensions
{
  public static class StreamExtensions
  {
    public static byte[] ToByteArray(this Stream stream)
    {
      stream.Position = 0;
      byte[] buffer = new byte[stream.Length];
      stream.Read(buffer, 0, buffer.Length);
      return buffer;
    }
  }
}
