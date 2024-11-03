using Dogs.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dogs.Host.Data.EntityConfigurations
{
	public class EntityDogConfiguration : IEntityTypeConfiguration<EntityDog> 
	{
		public void Configure(EntityTypeBuilder<EntityDog> builder)
		{
			builder.ToTable("Dogs");

			builder.Property(x => x.Id)
				.UseHiLo("dog_hilo")
				.IsRequired(true);

			builder.Property(x => x.Name)
				.IsRequired(true)
				.HasMaxLength(50);

			builder.Property(x => x.Color)
				.IsRequired(true);

			builder.Property(x => x.TailLength)
				.IsRequired(true);

			builder.Property(x => x.Weight)
				.IsRequired(true);
		}
	}
}
