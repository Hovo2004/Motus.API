using Motus.API.Data.DAO;
using Microsoft.EntityFrameworkCore;
using Motus.API.Core.Services.SignUpService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISignUpService, SignUpService>();

// Add DbContext with SQL Server connection and specify migrations assembly
builder.Services.AddDbContext<MainDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MainConnectionString"),
        b => b.MigrationsAssembly("Motus.API.Data"))); // Explicitly specify the migrations assembly

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
