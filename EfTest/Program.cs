using System.Data.Entity;

namespace EfTest
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());

			var tc = new TestClass();
			tc.CreateUsers();
			tc.Test();
		}
	}
}