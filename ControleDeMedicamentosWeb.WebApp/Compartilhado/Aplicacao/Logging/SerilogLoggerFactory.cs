using Serilog;
using Serilog.Core;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Aplicacao.Logging;

public static class SerilogLoggerFactory
{
    public static void AddSerilogLogger(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder logging
    )
    {
        Log.Logger = SerilogFactory.Create(configuration);

        // Remove o provedor padrão de logs da Microsoft
        logging.ClearProviders();

        services.AddSerilog(Log.Logger);
    }
}
