using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Standard.Specification
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        public string BuyerId { get; set; }

        public List<BasketItem> Items { get; set; }
    }

    public class BasketItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}