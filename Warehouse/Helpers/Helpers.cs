namespace Warehouse.Helpers;

public static class Helpers
{
	public static UserRole CurrentUserRole { get; set; }

	public enum UserRole
	{
		Coordinator,
		Employee
	}

	public class ItemToDisplay
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ItemGroup { get; set; }
		public string Unit { get; set; }
		public int Quantity { get; set; }
		public decimal PriceNoVat { get; set; }
		public string Status { get; set; }
		public string? StorageLocation { get; set; }
		public string? ContactPerson { get; set; }
	}
}
