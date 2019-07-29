using System;
using System.Linq.Expressions;

namespace Standard.Mapping
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Expression<Func<EntityDomain, bool>> accountModelQuery = a => a.Bal == 0 && a.Name != null && a.Id != 7;
            var accountQuery =
                accountModelQuery.Map<EntityDomain, EntityModel , EntityMapper>();
            Console.WriteLine($"Before: {accountModelQuery}");
            Console.WriteLine($"After : {accountQuery}");
            Console.ReadKey();
        }
    }
}
