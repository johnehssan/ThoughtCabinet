using Microsoft.EntityFrameworkCore;
using ThoughtCabinet.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();

// Add DbContext
builder.Services.AddDbContext<ThoughtsContext>(options =>
    options.UseSqlServer("Server=EHSSANJPC;Database=ThoughtCabinet;Trusted_Connection=True;TrustServerCertificate=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();