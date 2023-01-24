using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.BLL.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T,bool>> Criteria { get; }

        List<Expression<Func<T,object>>> Includes { get; }

        public Expression<Func<T,object>> OrderBy { get; }  

        public Expression<Func<T , object>> OrderByDesc { get; }

        public int Take { get; }

        public int Skip { get; }

        public bool IsPagingEnabled { get;  }
    }
}
