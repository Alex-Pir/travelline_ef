using Microsoft.EntityFrameworkCore;
using TodoBack.Infrastructure;
using TodoBack.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

string connectionString = builder.Configuration.GetConnectionString("TodoConnection");
builder.Services.AddDbContext<TodoDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<global::TodoBack.Repositories.ITodoRepository, global::TodoBack.Repositories.TodoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder =>
{
    builder
    .WithOrigins("localhost:4200")
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.Run();
