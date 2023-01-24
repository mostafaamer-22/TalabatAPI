using AutoMapper;
using AutoMapper.Execution;
using Talabat.DAL.Entities;
using Talabat.Dtos;

namespace Talabat.Helper
{


    public class ProductUrlReslover : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlReslover(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["ApiUrl"]}{source.PictureUrl}";
            }
            return null;
        }
    }
}
