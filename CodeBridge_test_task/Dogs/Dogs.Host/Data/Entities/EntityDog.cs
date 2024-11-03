namespace Dogs.Host.Data.Entities
{
	public class EntityDog
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;
		public string Color { get; set; } = null!;
		public double TailLength { get; set; }
		public double Weight { get; set; }
	}
}
