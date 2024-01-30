using Microsoft.EntityFrameworkCore;
using ServiceA.Clients;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<ServiceAClient>(client =>
{
    client.BaseAddress = new Uri("http://host.docker.internal:5000");
});

builder.Services.AddControllers();

/* Database Context Dependency Injection */
var dbHost = "sql-server-db";
var dbName = "random_numbers";
var dbPassword = "P@ssw0rd121#";
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

/* Redis Connection */
string connection = builder.Configuration.GetConnectionString("Redis");

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connection));

builder.Services.AddStackExchangeRedisCache(redisOptions => {
    redisOptions.Configuration = connection;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
