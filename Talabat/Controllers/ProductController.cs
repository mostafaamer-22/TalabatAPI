using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.BLL.Interfaces;
using Talabat.BLL.ProductSpecifications;
using Talabat.DAL.Entities;
using Talabat.Dtos;
using Talabat.Errors;
using Talabat.Helper;

namespace Talabat.Controllers
{
    [Authorize]
    public class ProductController : BaseControllerApi
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRespository;
        private readonly IGenericRepository<ProductType> _typesRespository;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> ProductRepository , 
            IGenericRepository<ProductBrand> BrandRespository ,
            IGenericRepository<ProductType> TypesRespository,
            IMapper mapper
            )
        {
            _productRepository = ProductRepository;
            _brandRespository = BrandRespository;
            _typesRespository = TypesRespository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productSpec)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productSpec);
            var Products = await _productRepository.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(Products);
            return Ok(new Pagination<ProductDto>(productSpec.PageIndex , productSpec.PageSize , data.Count , data));
        }

        [HttpGet(nameof(GetProductById))]
        public async Task<ActionResult<ProductDto>> GetProductById([FromQuery] int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetEntityWithSpec(spec);
            
            if(product == null)
                return NotFound(new ApiException(404));

            var data = _mapper.Map<Product , ProductDto>(product);
            return Ok(data);

        }

        [HttpGet("GetAllBrand")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrandProducts()
        {
            var Brands = await _brandRespository.GetAllAsync();
            return Ok(Brands);
        }

        [HttpGet(nameof(GetTypeProducts))]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypeProducts()
        {
            var Types = await _typesRespository.GetAllAsync();
            return Ok(Types);
        }

    }
}
