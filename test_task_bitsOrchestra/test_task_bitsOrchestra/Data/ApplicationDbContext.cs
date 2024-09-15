using Microsoft.EntityFrameworkCore;
using test_task_bitsOrchestra.Models;

namespace test_task_bitsOrchestra.Data
{
	public class ApplicationDbContext : DbContext 
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		
		public DbSet<Person> Persons { get; set; }
	}
}
