using NoticeApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS ��� ���
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder => 
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

AddDependencyInjectionContainerForNoticeApp(builder);

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