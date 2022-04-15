using Microsoft.EntityFrameworkCore;
using TodoBack.Infrastructure.Configurations;

namespace TodoBack.Infrastructure
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoConfiguration());
        }
    }
}
