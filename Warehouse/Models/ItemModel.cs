namespace Warehouse.Models;

public class ItemModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ItemGroupId { get; set; }
    public int UnitId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceNoVat { get; set; }
    public string Status { get; set; }
    public string? StorageLocation { get; set; }
    public string? ContactPerson { get; set; }
}
