using Microsoft.EntityFrameworkCore;
using testTaskReenbit.Data.Entities;

namespace testTaskReenbit.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }
    }
}
