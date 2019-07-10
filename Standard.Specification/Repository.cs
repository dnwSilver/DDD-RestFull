using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace Standard.Specification
{
    internal class Repository<TEntity>
        where TEntity : Basket
    {
        private readonly DbContext _dbContext = new StandardContext();

        public IEnumerable<TEntity> List(ISpecification<TEntity> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes =
                spec.Includes.Aggregate(_dbContext.Set<TEntity>().AsQueryable(),
                                        (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult =
                spec.IncludeStrings.Aggregate(queryableResultWithIncludes,
                                              (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult.Where(spec.Criteria).AsEnumerable();
        }
    }
}
