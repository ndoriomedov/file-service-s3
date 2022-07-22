namespace WebService.Extensions
{
  public static class StringExtensions
  {
    public static string? NullIfEmpty(this string? str)
    {
      if (str == string.Empty)
      {
        return null;
      }
      else
      {
        return str;
      }
    }
  }
}
