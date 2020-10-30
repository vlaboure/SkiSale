using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        // brand + type 
        public ProductWithBrandAndTypeSpecification(ProductSpecParams specParams)
            : base(x =>   
        #region 
                (string.IsNullOrEmpty(specParams.Search)||x.Name.ToLower()
                    .Contains(specParams.Search))&&
                (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId)&&
                (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId)
        #endregion
            )
        {   
            // AddInclude de BaseSpecification ajoute la spécification à la liste
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            AddOrderBy(p =>p.Name);
//ApplyPagination(take,skip)-->page 1: take 6 skip 0 
            ApplyPagination(specParams.ItemsPerPage,
                specParams.ItemsPerPage*(specParams.PageIndex-1));
            if(!string.IsNullOrEmpty(specParams.Sort))
            {

                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p =>p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(p =>p.Price);
                        break;

                    default : 
                        AddOrderBy(p =>p.Name);
                        break;
                }
            }

        }

        // brand + type + id
        public ProductWithBrandAndTypeSpecification(int id) : 
            base(x => x.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}