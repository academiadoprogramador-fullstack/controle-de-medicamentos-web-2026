using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Logging;

public static class SerilogFactory
{
    public static Logger Create(IConfiguration configuration)
    {
        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoArquivoLogs = Path.Combine(
            caminhoAppData,
            "ControleDeMedicamentosWeb",
            "erros.log"
        );

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                caminhoArquivoLogs,
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Error
            );

        NewRelicOptions newRelicOptions = configuration
            .GetSection(NewRelicOptions.SectionName)
            .Get<NewRelicOptions>() ?? new NewRelicOptions();

        string? licenseKey = newRelicOptions.LicenseKey ?? configuration["NEWRELIC_LICENSE_KEY"];

        if (string.IsNullOrWhiteSpace(licenseKey))
        {
            throw new InvalidOperationException(
                "A chave de licença do New Relic não foi configurada. Configure NewRelic:LicenseKey ou NEWRELIC_LICENSE_KEY."
            );
        }

        loggerConfiguration.WriteTo.NewRelicLogs(
            endpointUrl: newRelicOptions.EndpointUrl,
            applicationName: newRelicOptions.ApplicationName,
            licenseKey: licenseKey
        );

        return loggerConfiguration.CreateLogger();
    }
}
