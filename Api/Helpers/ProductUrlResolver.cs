using Api.Dtos;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Core.Entities;

namespace Api.Helpers
{
                        //IValueResolver<in TSource, in TDestination, TDestMember>
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        //IConfiguration pour 
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        //l'adresse 
        public string Resolve(Product source, ProductToReturnDto destination, 
            string destMember, ResolutionContext context)
        {
            //si adresse image non vide
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                var tt = _config["ApiUrl"] + source.PictureUrl;
                return tt;
            }
            return null;
        }
    }
}