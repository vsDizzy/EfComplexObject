using System.ComponentModel.DataAnnotations.Schema;

namespace EfTest
{
	[ComplexType]
	internal class Phone
	{
		public string Extension { get; set; }
		public string Number { get; set; }
	}
}