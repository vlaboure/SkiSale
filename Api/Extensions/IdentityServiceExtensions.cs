using System.Text;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions
{
    public static class IdentityServiceExtensions
    {
        // comme on ajoute à ConfigureServices de type IServiceCollection
        // on crée une méthode type IServiceCollection
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)// this permet d'instancier directement la méthode de la classe statique
        {
            //partie fonctionnant aussi sans EntityFramework
            var builder = services.AddIdentityCore<AppUser>();
                    //<TUser>
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            // pour valider l'utilisation du CRUD <TContext>
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
                    //<TSignInManager>
            builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer( opt => 
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],    
                        ValidateIssuer = true, 
                        ValidateAudience = false //evite bug si absent                     
                    };
                });  
            return services;
        }
    }
}