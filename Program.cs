using Microsoft.EntityFrameworkCore;
using ThoughtCabinet.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();