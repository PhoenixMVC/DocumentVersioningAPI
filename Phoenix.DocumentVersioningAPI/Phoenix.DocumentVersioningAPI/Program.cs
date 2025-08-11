using Microsoft.EntityFrameworkCore;
using Phoenix.Data;
using Phoenix.Data.Contracts;
using Phoenix.Data.Repositories;
using Phoenix.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped( typeof(IRepository<>), typeof(Repository<>)); 
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IStorageService>(sp =>
    new FileSystemStorageService(Path.Combine(Directory.GetCurrentDirectory(), "uploads")));
builder.Services.AddScoped<DocumentService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
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
