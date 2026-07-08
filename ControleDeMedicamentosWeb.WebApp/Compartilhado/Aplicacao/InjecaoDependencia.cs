using ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloEstoque;
using ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloFornecedor;
using ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloFuncionario;
using ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloMedicamento;
using ControleDeMedicamentosWeb.Aplicacao.Modulos.ModuloPaciente;
using ControleDeMedicamentosWeb.WebApp.Compartilhado.Aplicacao.Logging;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder logging
    )
    {
        services.AddSerilogLogger(configuration, logging);

        services.AddScoped<ServicoEstoque>();
        services.AddScoped<ServicoFornecedor>();
        services.AddScoped<ServicoFuncionario>();
        services.AddScoped<ServicoMedicamento>();
        services.AddScoped<ServicoPaciente>();
    }
}
