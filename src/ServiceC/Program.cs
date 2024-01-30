using ServiceB.Clients;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<ServiceBClient>(client =>
{
    client.BaseAddress = new Uri("http://host.docker.internal:5001");
});

builder.Services.AddControllers();

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
