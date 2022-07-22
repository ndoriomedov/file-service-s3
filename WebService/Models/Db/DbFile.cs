using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebService.Models.Db
{
  public class DbFile
  {
    public const string TableName = "Files";

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string Bucket { get; set; }
  }

  public class DbFileConfiguration : IEntityTypeConfiguration<DbFile>
  {
    public void Configure(EntityTypeBuilder<DbFile> builder)
    {
      builder.ToTable(DbFile.TableName);

      builder.HasKey(f => f.Id);

      builder.Property(f => f.Name).IsRequired();
      builder.Property(f => f.ContentType).IsRequired();
      builder.Property(f => f.Size).IsRequired();
      builder.Property(f => f.Bucket).IsRequired();
    }
  }
}
