using System.Data.Entity;

namespace EfTest
{
	internal class DataContext : DbContext
	{
		public DbSet<User> Users { get; set; }
	}
}