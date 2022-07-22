using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WebService.Models.Db;

namespace WebService.Npgsql.EntityFramework.Migrations
{
  [DbContext(typeof(FileServiceDbContext))]
  [Migration("202207201530000_Initial")]
  public class Initial : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: DbFile.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "text", nullable: false),
          ContentType = table.Column<string>(type: "text", nullable: false),
          Size = table.Column<string>(type: "bigint", nullable: false),
          Bucket = table.Column<string>(type: "text", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Files", x => x.Id);
        });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: DbFile.TableName);
    }
  }
}
