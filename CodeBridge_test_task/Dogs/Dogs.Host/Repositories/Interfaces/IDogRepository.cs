namespace Dogs.Host.Repositories.Interfaces
{
	public interface IDogRepository
	{
		Task<int?> AddDogAsync(string name, string color, double tailLength, double weight);
	}
}
