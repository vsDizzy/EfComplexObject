using System.ComponentModel.DataAnnotations.Schema;

namespace EfTest
{
	[ComplexType]
	internal class Address
	{
		public Address()
		{
			Phone1 = new Phone();
			Phone2 = new Phone();
		}

		public string City { get; set; }
		public string Street { get; set; }
		public string State { get; set; }
		public Phone Phone1 { get; set; }
		public Phone Phone2 { get; set; }
	}
}