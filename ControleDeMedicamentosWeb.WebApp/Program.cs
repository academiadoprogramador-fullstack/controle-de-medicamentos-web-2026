using ControleDeMedicamentosWeb.WebApp.Compartilhado.Aplicacao;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Sql;

var builder = WebApplication.CreateBuilder(args);

// Configuração de Dependências (Dependency Injection)
builder.Services.AddInfraRepositories();

builder.Services.AddApplicationServices(builder.Configuration, builder.Logging);

builder.Services.AddPresentationConfig(builder.Configuration);

// Health Check
builder.Services.AddHealthChecks()
    .AddCheck<SqlServerHealthCheck>("sqlserver-db-check", tags: ["ready"]);

var app = builder.Build();

// Configuração de Middlewares
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

// Mapeamento do endpoint do Health Check
app.MapHealthChecks("/health");

// Execução do Servidor
app.Run();
