namespace RetailInventory.Models;

public class CategoryProductsDTO
{
    public string CategoryName { get; set; } = string.Empty;
    public int ProductCount { get; set; }
    public decimal AveragePrice { get; set; }
}