using Dogs.Host.Data.Entities;
using Dogs.Host.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Dogs.Host.Data
{
	public class ApplicationDbContext: DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<EntityDog> EntityDogs { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new EntityDogConfiguration());
		}
	}
}
