using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Specification;
using Talabat.DAL.Entities;

namespace Talabat.BLL.ProductSpecifications
{
    public class ProductsWithTypesAndBrandsSpecification :BaseSpecification<Product> 
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams specParams)
            :base(
                 P => (!specParams.BrandId.HasValue || P.ProductBrandId == specParams.BrandId.Value)
                 && (!specParams.TypeID.HasValue || P.ProductTypeId == specParams.TypeID.Value) &&
                 (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search))
                 )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name);

            ApplyPaging(specParams.PageSize, specParams.PageSize * (specParams.PageIndex - 1));

            if(!string.IsNullOrEmpty(specParams.Sort))
            {
                switch(specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base( p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }


    }
}
