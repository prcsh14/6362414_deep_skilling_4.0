namespace RetailInventory.Models;

public class Inventory
{
    public int InventoryId { get; set; }
    public int QuantityInStock { get; set; }
    public DateTime LastRestocked { get; set; }
    public int MinimumStockLevel { get; set; }
    
    // Foreign key and navigation property
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}