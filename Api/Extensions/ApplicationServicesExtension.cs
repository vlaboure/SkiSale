using System.Linq;
using Api.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
                    //injection on scoped le repository est créé le temps de la requête
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                 //on configure le comportement pour passer dans ApiValidationError
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count >0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                            var errorResponse = new ApiValidationErrorResponse()
                            {
                                Errors = errors
                            };
                    return new BadRequestObjectResult(errorResponse);
                }; 
            });
            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>//OpenApiInfo--> fournit les metadata sur l'Api en cours 
              {c.SwaggerDoc("v1", new OpenApiInfo{Title = "Skinet Api", Version = "V1"});});
            return services;
        }
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();   //SwaggerEnpoint --> bas de page
            app.UseSwaggerUI(c => 
                {c.SwaggerEndpoint("/swagger/v1/swagger.json","Skinet Api v1");});
            return app;        
        }
    }
}