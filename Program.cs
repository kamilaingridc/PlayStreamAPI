using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlayStreamAPI.Repositories;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Registra o DbContext para MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Registra o reposit�rio como um servi�o
builder.Services.AddScoped<PlaylistRepository>();

builder.Services.AddAuthorization();

// Adiciona os servi�os do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlayStream API", Version = "v1" });
});

var app = builder.Build();

// Configura o pipeline da aplica��o

// Ativa o Swagger apenas no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlayStream API v1");
        c.RoutePrefix = string.Empty; // Coloca o Swagger na raiz do aplicativo
    });
}

// Usar a autoriza��o (se necess�rio no seu projeto)
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

// Inicia o aplicativo
app.Run();
