using PlayStreamAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registra o reposit�rio como um servi�o
builder.Services.AddScoped<PlaylistRepository>();

var app = builder.Build();

// Configura��es do pipeline da aplica��o
app.MapControllers();  // Mapeia os controladores

app.Run();
