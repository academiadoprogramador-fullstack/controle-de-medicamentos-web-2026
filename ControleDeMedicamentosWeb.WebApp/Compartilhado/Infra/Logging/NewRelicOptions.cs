namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Logging;

public sealed class NewRelicOptions
{
    public const string SectionName = "NewRelic";

    public string EndpointUrl { get; init; } = "https://log-api.newrelic.com/log/v1";
    public string ApplicationName { get; init; } = "controle-de-medicamentos-server";
    public string? LicenseKey { get; init; }
}
