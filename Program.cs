using HollyJukeBox.Endpoints;
using HollyJukeBox.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IArtistEndPoint, ArtistEndPoint>();
builder.Services.AddSingleton<IAlbumEndPoint, AlbumEndPoint>();

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

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseRouting();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();