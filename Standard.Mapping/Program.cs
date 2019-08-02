using System;
using System.Linq.Expressions;

namespace Standard.Mapping
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Expression<Func<IEntityDomain, bool>> accountModelQuery = a => a.Bal == 0 && a.Name != null && a.Id != 7;
            var accountQuery = accountModelQuery.Map<IEntityDomain , IEntityModel, EntityPropertyMapper>();
            Console.WriteLine($"Before: {accountModelQuery}");
            Console.WriteLine($"After : {accountQuery}");
            Console.ReadKey();
        }
    }
}
