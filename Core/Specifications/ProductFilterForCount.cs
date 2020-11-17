using Core.Entities;

namespace Core.Specifications
{
#region
 /*
 *****  classe pour afficher le nombre exact de produits et non le nombre par defaut
        en utilisant la classe pagination
        gérer les la recherche par mot
 */
 #endregion
    public class ProductFilterForCount: BaseSpecification<Product>
    {
        public ProductFilterForCount(ProductSpecParams specParams)
        : base(x =>   
                (string.IsNullOrEmpty(specParams.Search)||x.Name.ToLower()
                  .Contains(specParams.Search))&&
                (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId)&&
                (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId)
            )
        {
        }
    }
}