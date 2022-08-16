using Microsoft.EntityFrameworkCore;
using NoticeApp.Models;
using UploadApp.Models.Uploads;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS 사용 등록
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

AddDependencyInjectionContainerForNoticeApp(builder);
AddDependencyInjectionContainerForUploadApp(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

///
///NoticeApp
///
void AddDependencyInjectionContainerForNoticeApp(WebApplicationBuilder builder)
{
    builder.Services.AddEntityFrameworkSqlServer().AddDbContext<NoticeAppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddTransient<INoticeRepositoryAsync, NoticeRepositoryAsync>();
}

void AddDependencyInjectionContainerForUploadApp(WebApplicationBuilder builder)
{
    builder.Services.AddEntityFrameworkSqlServer().AddDbContext<UploadAppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddTransient<IUploadRepository, UploadRepository>();
}