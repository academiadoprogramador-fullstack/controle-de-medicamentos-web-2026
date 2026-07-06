using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Infra.Sql;

public sealed class SqlServerHealthCheck(IServiceProvider serviceProvider) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        ISqlConnectionFactory factory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

        try
        {
            using SqlConnection connection = factory.CreateConnection();

            await connection.OpenAsync(cancellationToken);

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT 1";

            await command.ExecuteScalarAsync(cancellationToken);

            return HealthCheckResult.Healthy("SQL Server está funcionando");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("SQL Server não está respondendo", ex);
        }
    }
}
