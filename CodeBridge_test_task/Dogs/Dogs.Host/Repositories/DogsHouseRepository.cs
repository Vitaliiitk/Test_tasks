using Dogs.Host.Data;
using Dogs.Host.Data.Entities;
using Dogs.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dogs.Host.Repositories
{
	public class DogsHouseRepository : IDogsHouseRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly ILogger<DogsHouseRepository> _logger;

		public DogsHouseRepository(ApplicationDbContext context, ILogger<DogsHouseRepository> logger)
		{
			_dbContext = context;
			_logger = logger;
		}

		public async Task<PaginatedItems<EntityDog>> GetByPageAsync(int pageIndex, int pageSize, string sortingAttribute, bool ascending)
		{
			IQueryable<EntityDog> query = _dbContext.EntityDogs;

			var totalItems = await query.LongCountAsync();

			_logger.LogInformation($"REPOSITORY method: Total items in database: {totalItems}");

			switch (sortingAttribute.ToLower())
			{
				case "name":
					query = ascending ? query.OrderBy(d => d.Name) : query.OrderByDescending(d => d.Name);
					break;
				case "color":
					query = ascending ? query.OrderBy(d => d.Color) : query.OrderByDescending(d => d.Color);
					break;
				case "taillength":
					query = ascending ? query.OrderBy(d => d.TailLength) : query.OrderByDescending(d => d.TailLength);
					break;
				case "weight":
					query = ascending ? query.OrderBy(d => d.Weight) : query.OrderByDescending(d => d.Weight);
					break;
				default:
					// In case invalid sorting attribute, default to sorting by Name
					query = query.OrderBy(d => d.Name);
					break;
			}

			var itemsOnPage = await query
			   .Skip(pageSize * pageIndex)
			   .Take(pageSize)
			   .ToListAsync();

			_logger.LogInformation($"REPOSITORY method: Items on page {pageIndex}: {itemsOnPage.Count}");

			return new PaginatedItems<EntityDog>() { TotalCount = totalItems, Data = itemsOnPage };
		}
	}
}
