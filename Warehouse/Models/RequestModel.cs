namespace Warehouse.Models;

public class RequestModel
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string EmployeeName { get; set; }
    public int UnitId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceNoVat { get; set; }
    public string? Comment { get; set; }
    public int StatusId { get; set; }
}
