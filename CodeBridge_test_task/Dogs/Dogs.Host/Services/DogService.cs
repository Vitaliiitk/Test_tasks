using Dogs.Host.Repositories.Interfaces;
using Dogs.Host.Services.Interfaces;

namespace Dogs.Host.Services
{
	public class DogService : IDogService
	{
		private readonly IDogRepository _dogRepository;

		private readonly ILogger<DogService> _logger;

		public DogService(IDogRepository dogRepository, ILogger<DogService> logger)
		{
			_dogRepository = dogRepository;
			_logger = logger;
		}

		public async Task<int?> AddDogAsync(string name, string color, double tailLength, double weight)
		{
			if (tailLength < 0)
			{
				throw new ArgumentException("Tail length cannot be negative.", nameof(tailLength));
			}

			if (weight < 0)
			{
				throw new ArgumentException("Weight cannot be negative.", nameof(weight));
			}

			var result = await _dogRepository.AddDogAsync(name, color, tailLength, weight);
			if (result is null)
			{
				throw new InvalidOperationException($"A dog with the name '{name}', color '{color}', weight '{weight}', tail length '{tailLength}' already exists.");
			}

			return result;
		}
	}
}
