using Microsoft.Extensions.DependencyInjection;
using RisCollect.Data;
using RisCollect.Forms;
using RisCollect.Services;

namespace RisCollect;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();

        services.AddSingleton<RisCollectDbContext>();
        services.AddSingleton<ArticleRepository>();
        services.AddSingleton<ArticleService>();
        services.AddSingleton<ExporterService>();
        services.AddSingleton<MainForm>();

        using var serviceProvider = services.BuildServiceProvider(
            new ServiceProviderOptions
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });

        var form = serviceProvider.GetRequiredService<MainForm>();

        Application.Run(form);
    }
}
