﻿namespace Warehouse.Models;

/// <summary>
/// Represents a row in the 'requests' table.
/// </summary>
public class RequestModel
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string EmployeeName { get; set; }
    public int Quantity { get; set; }
    public decimal PriceNoVat { get; set; }
    public string? CommentEmployee { get; set; }
    public string? CommentCoordinator { get; set; }
    public int StatusId { get; set; }
}
