using Redis.API.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RedisService constructor'da parametre istediði için kendimiz oluþturduk.
builder.Services.AddSingleton<RedisService>(options =>
{
    var redisConfig = $"{builder.Configuration["Redis:Host"]}:{builder.Configuration["Redis:Port"]}";
    return new RedisService(redisConfig);
});

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
