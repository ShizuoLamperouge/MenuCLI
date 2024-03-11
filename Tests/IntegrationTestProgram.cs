using MenuCLI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;
internal class IntegrationTestProgram
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

        await host.Services.StartMenu(true);
    }
}
