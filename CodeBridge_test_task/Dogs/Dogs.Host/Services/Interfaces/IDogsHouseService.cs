using Dogs.Host.Models.Dtos;
using Dogs.Host.Models.Responses;

namespace Dogs.Host.Services.Interfaces
{
	public interface IDogsHouseService
	{
		Task<PaginatedItemsResponse<DogsDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, string sortingAttribute, bool ascending);
	}
}
