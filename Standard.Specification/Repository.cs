using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace Standard.Specification
{
    internal class BasketRepository
    {
        private readonly DbContext _dbContext = new StandardContext();

        public IEnumerable<Basket> List(ISpecification<Basket> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes =
                spec.Includes.Aggregate(_dbContext.Set<Basket>().AsQueryable(),
                                        (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            // var secondaryResult =
             //   spec.IncludeStrings.Aggregate(queryableResultWithIncludes,
              //                                (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return queryableResultWithIncludes.Where(spec.Criteria).AsEnumerable();
        }
    }
}
