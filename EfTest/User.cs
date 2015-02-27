namespace EfTest
{
	internal class User
	{
		public User()
		{
			HomeAddress = new Address();
			WorkAddress = new Address();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public Address HomeAddress { get; set; }
		public Address WorkAddress { get; set; }
	}
}