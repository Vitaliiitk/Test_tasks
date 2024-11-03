using AutoMapper;
using Dogs.Host.Data;
using Dogs.Host.Data.Entities;
using Dogs.Host.Models.Dtos;
using Dogs.Host.Repositories.Interfaces;
using Dogs.Host.Services;
using Dogs.Host.Services.Interfaces;

namespace Dogs.UnitTests.Services
{
	public class DogsHouseServiceTest
	{
		private readonly IDogsHouseService _dogsHouseService;

		private readonly Mock<IMapper> _mapper;
		private readonly Mock<IDogsHouseRepository> _dogsHouseRepository;

		// Test data
		private static readonly int testTotalCount = 2;

		private readonly PaginatedItems<EntityDog> paginatedItemsSuccess = new PaginatedItems<EntityDog>()
		{
			Data = new List<EntityDog>
				{
					new EntityDog() { Id = 10, Name = "TestName", Color = "TestColor", TailLength = 10, Weight = 35},
					new EntityDog() { Id = 11, Name = "TestName2", Color = "TestColor2", TailLength = 11, Weight = 36}
				},
			TotalCount = testTotalCount
		};

		public DogsHouseServiceTest()
		{
			_dogsHouseRepository = new Mock<IDogsHouseRepository>();
			_mapper = new Mock<IMapper>();

			_mapper.Setup(m => m.Map<DogsDto>(
				It.IsAny<EntityDog>()))
				.Returns((EntityDog dog) => new DogsDto
				{
					Id = dog.Id,
					Name = dog.Name,
					Color = dog.Color,
					TailLength = dog.TailLength,
					Weight = dog.Weight
				});

			_dogsHouseService = new DogsHouseService(_dogsHouseRepository.Object, _mapper.Object);
		}

		[Fact]
		public async Task GetCatalogItemsAsync_Success()
		{
			// arrange
			var testPageIndex = 0;
			var testPageSize = 2;
			var testSortingAttribute = "Name";
			var testAscending = true;

			_dogsHouseRepository.Setup(s => s.GetByPageAsync(testPageIndex, testPageSize, testSortingAttribute, testAscending))
								.ReturnsAsync(paginatedItemsSuccess);

			//act
			var result = await _dogsHouseService.GetCatalogItemsAsync(testPageSize, testPageIndex, testSortingAttribute, testAscending);

			//assert
			result.Should().NotBeNull();
			result?.Data.Should().NotBeNull();
			result?.Count.Should().Be(testTotalCount);
			result?.PageSize.Should().Be(testPageSize);
			result?.PageIndex.Should().Be(testPageIndex);
		}

		[Fact]
		public async Task GetCatalogItemsAsync_Failed()
		{
			// arrange
			var testPageIndex = 0;
			var testPageSize = 2;
			var testSortingAttribute = "Name";
			var testAscending = true;

			_dogsHouseRepository.Setup(s => s.GetByPageAsync(testPageIndex, testPageSize, testSortingAttribute, testAscending))
				.ReturnsAsync((PaginatedItems<EntityDog>?)null);

			// act
			var result = await _dogsHouseService.GetCatalogItemsAsync(testPageSize, testPageIndex, testSortingAttribute, testAscending);

			// assert
			result.Should().BeNull();
		}

	}
}
