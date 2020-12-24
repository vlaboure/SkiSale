using Api.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //source destination
            CreateMap<Product, ProductToReturnDto>()
        //on désire importer le string depuis la clé étrangère et pas l'objet
                        //destination	     action/option       source à appliquer
                .ForMember(p => p.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
//pour éviter de retaper la ligne inversée ReverseMap --> pour mapper dans les deux sens   
                         
        }
    }
}