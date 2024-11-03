using System.ComponentModel.DataAnnotations;

namespace Dogs.Host.Models.Requests
{
	public class AddDogRequest
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Color { get; set; } = null!;

		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "Tail length must be a positive number.")]
		public double TailLength { get; set; }

		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "Tail length must be a positive number.")]
		public double Weight { get; set; }
	}
}
