
using Microsoft.EntityFrameworkCore;

namespace Standard.Specification
{
    public class StandardContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.;Database=Standard;Integrated Security=True");
        }
    }

}
