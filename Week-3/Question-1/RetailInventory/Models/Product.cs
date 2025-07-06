namespace RetailInventory.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string SKU { get; set; } = string.Empty;
    
    // Foreign key and navigation property
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    // Navigation property for inventory
    public Inventory? Inventory { get; set; }
}