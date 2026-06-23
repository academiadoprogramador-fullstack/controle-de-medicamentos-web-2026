using Serilog;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Aplicacao.Logging;

public static class SerilogLoggerFactory
{
    public static void AddSerilogLogger(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder logging
    )
    {
        services.Configure<NewRelicOptions>(configuration.GetSection(NewRelicOptions.SectionName));

        Log.Logger = SerilogFactory.Create(configuration);

        logging.ClearProviders();

        services.AddSerilog(Log.Logger, dispose: true);
    }
}
