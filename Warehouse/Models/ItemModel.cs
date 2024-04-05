﻿namespace Warehouse.Models;

public class ItemModel
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