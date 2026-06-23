using ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao.Mapeamento;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao;

public static class InjecaoDependencia
{
    public static void AddPresentationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews().AddRazorOptions(options =>
        {
            // Reseta a configuração padrão do MVC
            options.ViewLocationFormats.Clear();

            // Localização das Views dos módulos: Modulos/ModuloCaixa/Apresentacao/Views/Listar.cshtml
            options.ViewLocationFormats.Add("/Modulos/Modulo{1}/Apresentacao/Views/{0}.cshtml");

            // Localização das Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
            options.ViewLocationFormats.Add("/Compartilhado/Apresentacao/Views/{0}.cshtml");
        });

        services.Configure<AutoMapperOptions>(configuration.GetSection(AutoMapperOptions.SectionName));

        services.AddAutoMapper(mapperConfig =>
        {
            string? licenseKey = configuration
                .GetSection(AutoMapperOptions.SectionName)
                .Get<AutoMapperOptions>()?
                .LicenseKey ?? configuration["AUTOMAPPER_LICENSE_KEY"];

            if (!string.IsNullOrWhiteSpace(licenseKey))
                mapperConfig.LicenseKey = licenseKey;

            mapperConfig.AddMaps(typeof(Program));
        });
    }
}
