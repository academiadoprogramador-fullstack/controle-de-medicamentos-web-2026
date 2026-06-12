using ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Arquivos;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services)
    {
        services.AddScoped(provider =>
        {
            ContextoJson contextoJson = new ContextoJson();

            contextoJson.Carregar();

            return contextoJson;
        });
    }
}
