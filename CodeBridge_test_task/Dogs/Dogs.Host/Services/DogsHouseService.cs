using AutoMapper;
using Dogs.Host.Models.Dtos;
using Dogs.Host.Models.Responses;
using Dogs.Host.Repositories.Interfaces;
using Dogs.Host.Services.Interfaces;

namespace Dogs.Host.Services
{
	public class DogsHouseService : IDogsHouseService
	{
		private readonly IDogsHouseRepository _dogsHouseRepository;
		private readonly IMapper _mapper;
		
		public DogsHouseService(
			IDogsHouseRepository dogsHouseRepository,
			IMapper mapper)
		{
			_dogsHouseRepository = dogsHouseRepository;
			_mapper = mapper;
		}

		public async Task<PaginatedItemsResponse<DogsDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, string sortingAttribute, bool ascending)
		{
			try
			{
				var result = await _dogsHouseRepository.GetByPageAsync(pageIndex, pageSize, sortingAttribute, ascending);
				if (result == null)
				{
					return null;
				}

				return new PaginatedItemsResponse<DogsDto>()
				{
					Count = result.TotalCount,
					Data = result.Data.Select(s => _mapper.Map<DogsDto>(s)).ToList(),
					PageIndex = pageIndex,
					PageSize = pageSize
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return null;
			}
		}
	}
}
