using MenuCLI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sandbox;
using System.Runtime.CompilerServices;

namespace Sandbox;

public class Program
{
    public static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddMenuCLI<MainMenu>();

                services.AddTransient<DependancyInjectionExemple>();
            })
            .Build();

        await host.Services.StartMenu();
    }
}
