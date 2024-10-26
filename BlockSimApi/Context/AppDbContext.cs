using BlockSimApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlockSimApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Block> Blocks { get; set; }
    }
}
