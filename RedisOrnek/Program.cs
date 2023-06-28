using Microsoft.EntityFrameworkCore;
using Redis.Cache;
using RedisOrnek.Database;
using RedisOrnek.Repositories;
using RedisOrnek.Services;
using StackExchange.Redis;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using RedisOrnek.Container;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("myDatabase");
});
builder.Services.AddSingleton<RedisService>(sp =>
{
    return new RedisService(builder.Configuration["Redis:Connection"]);
});

// IDatabase istnirse RedisDatabase verilir.
builder.Services.AddSingleton<IDatabase>(sp =>
{
    var redisService = sp.GetRequiredService<RedisService>();
    return redisService.GetDatabase(1);
});

builder.Services.AddScoped<IProductRepository>(sp =>
{
    var appDbContext = sp.GetRequiredService<AppDbContext>();

    var productReository = new ProductRepository(appDbContext);

    var redisDatabase = sp.GetRequiredService<IDatabase>();

    // --
    return new ProductRepositoryWithCache(productReository, redisDatabase);

});

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// uygulama ayaða kalkarken db oluþturulur
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

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
