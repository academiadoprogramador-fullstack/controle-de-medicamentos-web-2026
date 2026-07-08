using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloEstoque;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFornecedor;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloFuncionario;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloMedicamento;
using ControleDeMedicamentosWeb.Dominio.Modulos.ModuloPaciente;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Arquivos;
using ControleDeMedicamentosWeb.Infra.Compartilhado.Sql;
using ControleDeMedicamentosWeb.Infra.Modulos.ModuloEstoque;
using ControleDeMedicamentosWeb.Infra.Modulos.ModuloFornecedor;
using ControleDeMedicamentosWeb.Infra.Modulos.ModuloFuncionario;
using ControleDeMedicamentosWeb.Infra.Modulos.ModuloMedicamento;
using ControleDeMedicamentosWeb.Infra.Modulos.ModuloPaciente;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped<ContextoJson>();

        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IRepositorioRequisicao, RepositorioRequisicaoEmSql>();
        services.AddScoped<IRepositorioFornecedor, RepositorioFornecedorEmSql>();
        services.AddScoped<IRepositorioFuncionario, RepositorioFuncionarioEmSql>();
        services.AddScoped<IRepositorioMedicamento, RepositorioMedicamentoEmSql>();
        services.AddScoped<IRepositorioPaciente, RepositorioPacienteEmSql>();
    }
}