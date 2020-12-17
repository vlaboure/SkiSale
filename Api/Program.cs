using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                // ILoggerFactory pour le jounal des log
                var loggerFactor = services.GetRequiredService<ILoggerFactory>();
                // crée la base si n'existe pas
                try
                {
                    //ajout des données et création de la base skinet.db
                    // et des tables si besoin
                    var context = services.GetRequiredService<StoreContext>();
                    await context.Database.MigrateAsync();
                    // appel de la méthode de seed
                    await StoreContextSeed.SeedAsync(context, loggerFactor);

                
                    //ajout des données et création de la base skinet.db
                    // et des tables si besoin
                    var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();                 
                    await identityContext.Database.MigrateAsync();
                    // appel de la methode seed
                    await AppIdentityDbContextSeed.SeedUserAsync(userManager);
                }
                catch (Exception e)
                {
                    var logger = loggerFactor.CreateLogger<Program>();
                    logger.LogError(e,"Erreur creation base" ); 
                }   
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
