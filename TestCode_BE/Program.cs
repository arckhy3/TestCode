using TestCode_BE;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
string dbDatabase = Environment.GetEnvironmentVariable("DB_DATABASE");


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer($"Server={dbServer};Database={dbDatabase};Trusted_Connection=True;TrustServerCertificate=True;"));


builder.Services.AddControllers();
//builder.Services.AddDbContext<DataContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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
