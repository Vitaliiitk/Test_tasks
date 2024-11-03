using Dogs.Host.Data.Entities;
using Dogs.Host.Repositories.Interfaces;
using Dogs.Host.Services.Interfaces;
using Dogs.Host.Services;
using Microsoft.Extensions.Logging;

namespace Dogs.UnitTests.Services
{
	public class DogServiceTest
	{
		private readonly IDogService _dogService;

		private readonly Mock<ILogger<DogService>> _logger;
		private readonly Mock<IDogRepository> _dogRepository;

		// Test data
		private readonly EntityDog _testDog = new EntityDog() { Name = "Name", Color = "test color", TailLength = 25, Weight = 40 };

		public DogServiceTest()
		{
			_dogRepository = new Mock<IDogRepository>();
			_logger = new Mock<ILogger<DogService>>();

			_dogService = new DogService(_dogRepository.Object, _logger.Object);
		}

		[Fact]
		public async Task AddDogAsync_Success()
		{
			// arrange
			var testIdResult = 13;

			_dogRepository.Setup(s => s.AddDogAsync(_testDog.Name, _testDog.Color, _testDog.TailLength, _testDog.Weight))
								.ReturnsAsync(testIdResult);

			//act
			var result = await _dogService.AddDogAsync(_testDog.Name, _testDog.Color, _testDog.TailLength, _testDog.Weight);

			//assert
			result.Should().NotBeNull();
			result.Should().Be(testIdResult);
		}

		[Fact]
		public async Task AddDogAsync_Failed()
		{
			// arrange
			int? testIdResult = null;

			_dogRepository.Setup(s => s.AddDogAsync(_testDog.Name, _testDog.Color, _testDog.TailLength, _testDog.Weight))
				.ReturnsAsync(testIdResult);

			// act & assert
			await Assert.ThrowsAsync<InvalidOperationException>(async () => 
				await _dogService.AddDogAsync(_testDog.Name, _testDog.Color, _testDog.TailLength, _testDog.Weight));
		}

	}
}
