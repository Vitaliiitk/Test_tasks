using Dogs.Host.Data;
using Dogs.Host.Data.Entities;
using Dogs.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dogs.Host.Repositories
{
	public class DogRepository : IDogRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly ILogger<DogRepository> _logger;

		public DogRepository(ApplicationDbContext context, ILogger<DogRepository> logger)
		{
			_dbContext = context;
			_logger = logger;
		}

		public async Task<int?> AddDogAsync(string name, string color, double tailLength, double weight)
		{
			try 
			{
				var existingDog = await _dbContext.EntityDogs.FirstOrDefaultAsync(d => d.Name == name && d.Color == color && d.TailLength == tailLength && d.Weight == weight);
				if (existingDog != null)
				{
					return null;
				}

				var newDog = new EntityDog
				{
					Name = name,
					Color = color,
					TailLength = tailLength,
					Weight = weight
				};
				var dog = await _dbContext.AddAsync(newDog);

				await _dbContext.SaveChangesAsync();

				return dog.Entity.Id;
			}
			catch (Exception ex) 
			{
				_logger.LogError(ex, "An error occurred while adding a new dog to the database.");
				return null;
			}
			
		}
	}
}
