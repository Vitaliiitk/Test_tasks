using Dogs.Host.Data.Entities;

namespace Dogs.Host.Data
{
	public static class DbInitializer
	{
		public static async Task Initialize(ApplicationDbContext context)
		{
			await context.Database.EnsureCreatedAsync();

			if (!context.EntityDogs.Any())
			{
				await context.EntityDogs.AddRangeAsync(GetPreconfiguredEntityDogs());

				await context.SaveChangesAsync();
			}
		}

		private static IEnumerable<EntityDog> GetPreconfiguredEntityDogs()
		{
			return new List<EntityDog>()
			{
				new EntityDog{ Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32},
				new EntityDog{ Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14}
			};
		}
	}
}
