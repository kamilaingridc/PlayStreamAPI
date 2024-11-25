using PlayStreamAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registra o repositório como um serviço
builder.Services.AddScoped<PlaylistRepository>();

var app = builder.Build();

// Configurações do pipeline da aplicação
app.MapControllers();  // Mapeia os controladores

app.Run();
