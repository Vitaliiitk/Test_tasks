namespace Dogs.Host.Services.Interfaces
{
	public interface IDogService
	{
		Task<int?> AddDogAsync(string name, string color, double tailLength, double weight);
	}
}
