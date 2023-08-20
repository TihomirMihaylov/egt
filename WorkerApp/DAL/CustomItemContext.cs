using Microsoft.EntityFrameworkCore;
using WorkerApp.Models;

namespace WorkerApp.DAL
{
    public class CustomItemContext : DbContext
    {
        public DbSet<CustomItem> Items { get; set; }

        public CustomItemContext(DbContextOptions<CustomItemContext> options)
            : base(options) { }
    }
}
