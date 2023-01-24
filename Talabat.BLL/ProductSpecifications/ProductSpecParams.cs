using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.BLL.ProductSpecifications
{
    public class ProductSpecParams
    {
        public int? BrandId { get; set; }

        public  int? TypeID { get; set; }

        public string? Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        private const int MaxPageSize = 50;

        private int _pageSize  = 5;

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


        public int PageSize
        {
            get { return _pageSize; }
            set 
            {
            
                _pageSize = value > MaxPageSize ?  MaxPageSize : value;
            }
        }

    }
}
