using System.Configuration;
using GoldenPrice;
using GoldenPrice.Application;
using GoldenPrice.Application.GetQuery;
using GoldenPrice.Provider;
using GoldenPrice.Provider.CacheLayer;
using Infrastructure;
using Infrastructure.CacheLayer;
using Infrastructure.Provider.brsapi.ir;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add memory cache
builder.Services.AddMemoryCache();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddHttpClient<BRSMarketDataProviderService>(client =>
{
    // client.BaseAddress = new Uri("https://brsapi.ir/");
    client.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Register MediatR handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMarketDataQuery).Assembly));

// Register market data providers
builder.Services.AddScoped<BRSMarketDataProviderService>();
builder.Services.AddScoped<IMarketDataProviderFactory, MarketDataProviderFactory>();
builder.Services.AddScoped<IMarketDataProvider, BRSMarketDataProviderService>();
builder.Services.AddScoped<IMarketDataService, MarketDataService>(); // Add this line

// Register cache services

var redisConnectionString = builder.Configuration["Redis:ConnectionString"];

if (string.IsNullOrWhiteSpace(redisConnectionString))
{
    throw new ApplicationException("Redis connection string is not configured");
}

builder.Services.AddSingleton<ICacheServiceFactory, CacheServiceFactory>();
builder.Services.AddSingleton<InMemoryCacheService>();
builder.Services.AddSingleton<RedisCacheService>();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<ICacheService, InMemoryCacheService>();
}
else
{
    builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
        ConnectionMultiplexer.Connect(builder.Configuration["Redis:ConnectionString"]!));
    builder.Services.AddScoped<ICacheService, RedisCacheService>();
}

// Configure cache settings
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("Cache"));

// Register CachedMarketDataProvider
builder.Services.AddScoped<IMarketDataProvider>(provider =>
{
    var factory = provider.GetRequiredService<IMarketDataProviderFactory>();
    var cacheService = provider.GetRequiredService<ICacheService>();
    var defaultProvider = builder.Configuration["MarketData:DefaultProvider"] ?? "BRS";

    return new CachedMarketDataProvider(factory, cacheService, defaultProvider);
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "GoldenPrice API", Version = "v1"});
});

var app = builder.Build();

// Configure middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GoldenPrice API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();