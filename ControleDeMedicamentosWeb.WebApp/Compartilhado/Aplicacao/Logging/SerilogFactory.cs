using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Aplicacao.Logging;

public static class SerilogFactory
{
    public static Logger Create(IConfiguration configuration)
    {
        IConfigurationSection newRelicSection = configuration.GetSection(NewRelicOptions.SectionName);

        NewRelicOptions newRelicOptions = newRelicSection.Get<NewRelicOptions>() ?? new NewRelicOptions();

        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string caminhoArquivoLogs = Path.Combine(caminhoAppData, "ControleDeMedicamentosWeb", "erros.log");

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                caminhoArquivoLogs,
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Error
            );

        bool deveEnviarParaNewRelic = newRelicSection.Exists() && newRelicOptions.Enabled;

        if (!deveEnviarParaNewRelic)
            return loggerConfiguration.CreateLogger();

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
