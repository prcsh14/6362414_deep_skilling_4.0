namespace RetailInventory_New.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Initialize with empty string
        public List<Product> Products { get; set; } = new(); // Initialize empty list
    }
}