using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class IdentityServiceExtensions
    {

        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            //partie fonctionnant aussi sans EntityFramework
            var builder = services.AddIdentityCore<AppUser>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            // pour valider l'utilisation du CRUD
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication();  
            return services;
        }
    }

}