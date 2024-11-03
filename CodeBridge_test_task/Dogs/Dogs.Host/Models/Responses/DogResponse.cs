namespace Dogs.Host.Models.Responses
{
	public class DogResponse<T>
	{
		public IEnumerable<T> Data { get; init; } = null!;
	}
}
