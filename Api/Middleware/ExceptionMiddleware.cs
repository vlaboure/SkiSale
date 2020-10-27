using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Middleware
{
    // erreur 500   
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, 
            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                            // ContentType--> definit le type de retour
                context.Response.ContentType = "application/json";
                //si erreur c'est une erreur 500
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    // selon si on est en dev ou non on affiche le détail erreur
                    // response contient le retour de l'erreur
                var response = _env.IsDevelopment()//!!! pas d'affichage de deta
                ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message);
                    // on désire afficher en mode camelcase
                var option = new JsonSerializerOptions{PropertyNamingPolicy=
                    JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(response, option);
                // ecriture de l'erreur en format json dans context 
                await context.Response.WriteAsync(json);
            }
        }
    }
}