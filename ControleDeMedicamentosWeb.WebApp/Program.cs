using ControleDeMedicamentosWeb.WebApp.Compartilhado.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Configuração de Dependências (Dependency Injection)
builder.Services.AddInfraRepositories(builder.Configuration, builder.Logging);

builder.Services.AddApplicationServices();

builder.Services.AddPresentationConfig();

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();
