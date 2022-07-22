using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using WebService.Business;
using WebService.Business.Interfaces;
using WebService.Extensions;
using WebService.Mappers.Db;
using WebService.Mappers.Db.Interfaces;
using WebService.Mappers.Dto;
using WebService.Mappers.Dto.Interfaces;
using WebService.Minio;
using WebService.Minio.Interfaces;
using WebService.Models.Configurations;
using WebService.Npgsql.EntityFramework;
using WebService.Sql.Provider;
using WebService.Sql.Provider.Repositories;
using WebService.Sql.Provider.Repositories.Interfaces;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<FormOptions>(options =>
{
  options.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.Configure<MinioConfiguration>(builder.Configuration.GetSection(MinioConfiguration.SectionName));

string NpgsqlConnectionString = Environment.GetEnvironmentVariable("NpgsqlConnectionString").NullIfEmpty()
  ?? builder.Configuration.GetConnectionString("NpgsqlConnectionString");
builder.Services.AddDbContext<FileServiceDbContext>(options => options.UseNpgsql(NpgsqlConnectionString));

AddServices(builder.Services);

var app = builder.Build();

UpdateDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void UpdateDatabase(IApplicationBuilder app)
{
  using var serviceScope = app.ApplicationServices
    .GetRequiredService<IServiceScopeFactory>()
    .CreateScope();

  using (FileServiceDbContext context = serviceScope.ServiceProvider.GetService<FileServiceDbContext>())
  {
    context.Database.Migrate();
  }
}

void AddServices(IServiceCollection services)
{
  services.AddHttpContextAccessor();

  services.AddSingleton<IMinioHelper, MinioHelper>();

  services.AddScoped<IDataProvider, FileServiceDbContext>();

  services.AddTransient<IFileRepository, FileRepository>();
  services.AddTransient<IUploadFileCommand, UploadFileCommand>();
  services.AddTransient<IGetFileCommand, GetFileCommand>();
  services.AddTransient<IFindFilesCommand, FindFilesCommand>();
  services.AddTransient<IDeleteFileCommand, DeleteFileCommand>();
  services.AddTransient<IDbFileMapper, DbFileMapper>();
  services.AddTransient<IFileResponseMapper, FileResponseMapper>();
}