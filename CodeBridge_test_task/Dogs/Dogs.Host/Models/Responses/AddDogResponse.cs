namespace Dogs.Host.Models.Responses
{
	public class AddDogResponse<T>
	{
		public T Id { get; set; } = default(T)!;
	}
}
