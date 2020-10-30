using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Errors;
using Api.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrand;
        private readonly IGenericRepository<ProductType> _productType;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<ProductBrand> productBrand, 
            IGenericRepository<ProductType> productType,IMapper mapper)
        {
            _productRepo = productRepo;
            _productBrand = productBrand;
            _productType = productType;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductSpecParams specParams)
        {
            // pour ajouter les spécifications marque/type
            var spec = new ProductWithBrandAndTypeSpecification(specParams);
            var Countspec = new ProductFilterForCount(specParams);
            // pour récupérer la quantité réelle d'items
            var totalItem = await _productRepo.CountAsync(Countspec); //Countspec
            // on récupère les specifications

            var products = await _productRepo.ListAsyncWithSpec(spec);   
          //  var productsDto = _mapper.Map<IEnumerable<>>  
            var datas = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);         
            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, 
                specParams.ItemsPerPage,totalItem,datas));       
        }

        [HttpGet("{id}")]
        // précise le type de réponse dans swagger
        [ProducesResponseType(StatusCodes.Status200OK)]
           // précise le type de réponse dans swagger pour l'erreur redirigée 
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task <ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            //id--> specification passé à BaseSpecifiction avec base(x => x.Id == id)
            //dans ProductWithBrandAndTypeSpecification où on utilise la méthode 
            // AddInclude de BaseSpecification méthode de délégué 
            //de  public List<Expression<Func<T, object>>> Includes{get;}
            //BaseSpecification<T> implémentant ISpecification<T>
            var spec = new ProductWithBrandAndTypeSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);
            if(product == null)
                return NotFound(new ApiResponse(404)); 
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand()
        {
            return Ok(await _productBrand.ListAllAsync());
            //return Ok(productBrands)
        } 
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType()
        {
            return Ok(await _productType.ListAllAsync());
            //return Ok(productBrands)
        } 

    }
}