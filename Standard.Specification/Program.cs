using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Standard.Specification
{
    internal class Program
    {
        private static bool isWork = true;
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            while (isWork)
            {
                var command = Console.ReadLine();

                switch(command)
                {
                    case "Add":
                        AddItemToBasket();
                        break;
                    case "Find":
                        ReadItems();
                        break;
                    case "q":
                        isWork = true;
                        break;
                    case "":

                        break;
                    default:
                        WriteError("Bad command. For view command list use command 'help'");
                        break;
                }
            }
        }

        private static void WriteError(string errorMessage)
        {
            Write(ConsoleColor.DarkRed, errorMessage);
        }
        private static void WriteSuccess(string successMessage)
        {
            Write(ConsoleColor.Green, successMessage);
        }

        private static void Write(ConsoleColor consoleColor, string message)
        {
            var previousForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = previousForegroundColor;
        }

        private static void ReadItems()
        {
            var repo = new BasketRepository();
            var spec = new BasketWithItemsSpecification(2);
            var result = repo.List(spec);

            foreach(var basket in result)
            {
                Console.WriteLine($"Basket #{basket.Id}");
                foreach(var item in basket.Items)
                {
                    Console.WriteLine($"Item #{item.Id} {item.Name}");
                }
            }

            if (!result.Any())
                Console.WriteLine("Ничего не нашли.");
        }

        private static void AddItemToBasket()
        {
            using (var context = new StandardContext())
            {
                context.Database.Migrate();
            }

            string itemName = "Хрень какая-то.";
            using (var db = new StandardContext())
            {
                var basket = new Basket
                             {
                                 BuyerId = "ыфввфы",
                                 Items = new List<BasketItem>
                                         {
                                             new BasketItem
                                             {
                                                 Name = itemName
                                             }
                                         }
                             };
                db.Baskets.Add(basket);
                db.SaveChanges();
                WriteSuccess($"В корзину добавлен итем \"{itemName}\".");
            }
        }
    }
}
