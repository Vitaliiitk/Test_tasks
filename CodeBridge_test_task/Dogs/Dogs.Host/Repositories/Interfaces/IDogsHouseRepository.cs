using Dogs.Host.Data;
using Dogs.Host.Data.Entities;

namespace Dogs.Host.Repositories.Interfaces
{
	public interface IDogsHouseRepository
	{
		Task<PaginatedItems<EntityDog>> GetByPageAsync(int pageIndex, int pageSize, string sortingAttribute, bool ascending);
	}
}
