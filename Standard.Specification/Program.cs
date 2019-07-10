using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Standard.Specification
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using(var context = new StandardContext())
            {
                context.Database.Migrate();
            }

            using(var db = new StandardContext())
            {
                var basket = new Basket
                             {
                                 BuyerId = "ыфввфы",
                                 Items = new List<BasketItem>
                                         {
                                             new BasketItem
                                             {
                                                 Name = "Хрень какая-то."
                                             }
                                         }
                             };
                db.Baskets.Add(basket);
                db.SaveChanges();
            }

            var repo = new Repository<Basket>();
            var spec = new BasketWithItemsSpecification(1);
            var result = repo.List(spec);

            foreach(var basket in result)
                Console.WriteLine(basket.Id);

            if(!result.Any())
                Console.WriteLine("Ничего не нашли.");

            Console.ReadKey();
        }
    }
}
