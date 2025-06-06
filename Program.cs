using HollyJukeBox.Data;
using HollyJukeBox.Endpoints;
using HollyJukeBox.Repository;
using HollyJukeBox.Services;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddScoped<DbInitializer>();
builder.Services.AddScoped<System.Data.IDbConnection>(sp =>
    new SqliteConnection(builder.Configuration.GetConnectionString("HollyJukeBoxDb")));
builder.Services.AddScoped<IArtistEndPoint, ArtistEndPoint>();
builder.Services.AddScoped<ICoverArtEndPoint, CoverArtEndPoint>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<ICoverArtRepository, CoverArtRepository>();
builder.Services.AddScoped<IArtistInfoRepository, ArtistInfoRepository>();
builder.Services.AddScoped<IAlbumInfoRepository, AlbumInfoRepository>();

builder.Services.AddSingleton<IMemoryCashingService, MemoryCashingService>();
builder.Services.AddSingleton<IRetryPolicyService, RetryPolicyService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConnections();
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddHttpClient<IArtistEndPoint, ArtistEndPoint>(client =>
{
    client.DefaultRequestHeaders.Add("User-Agent", "HollyJukeBox");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbInit = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await dbInit.EnsureTablesCreatedAsync();
}

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseRouting();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();