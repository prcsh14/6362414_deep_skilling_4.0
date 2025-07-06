// using Microsoft.EntityFrameworkCore;
// using RetailInventory.Data;
// using RetailInventory.Models;

// class Program
// {
//     static void Main(string[] args)
//     {
//         using var db = new RetailDbContext();
        
//         // Ensure database is created
//         db.Database.EnsureCreated();
        
//         Console.WriteLine("Retail Inventory Management System");
//         Console.WriteLine("1. Add Category");
//         Console.WriteLine("2. Add Product");
//         Console.WriteLine("3. View Inventory");
//         Console.Write("Select an option: ");
        
//         var option = Console.ReadLine();
        
//         switch (option)
//         {
//             case "1":
//                 AddCategory(db);
//                 break;
//             case "2":
//                 AddProduct(db);
//                 break;
//             case "3":
//                 ViewInventory(db);
//                 break;
//             default:
//                 Console.WriteLine("Invalid option");
//                 break;
//         }
//     }

//     static void AddCategory(RetailDbContext db)
//     {
//         Console.Write("Enter category name: ");
//         var name = Console.ReadLine() ?? string.Empty;
        
//         Console.Write("Enter category description: ");
//         var description = Console.ReadLine() ?? string.Empty;
        
//         var category = new Category
//         {
//             Name = name,
//             Description = description
//         };
        
//         db.Categories.Add(category);
//         db.SaveChanges();
        
//         Console.WriteLine("Category added successfully!");
//     }

//     static void AddProduct(RetailDbContext db)
//     {
//         Console.Write("Enter product name: ");
//         var name = Console.ReadLine() ?? string.Empty;
        
//         Console.Write("Enter product description: ");
//         var description = Console.ReadLine() ?? string.Empty;
        
//         Console.Write("Enter product price: ");
//         if (!decimal.TryParse(Console.ReadLine(), out var price))
//         {
//             Console.WriteLine("Invalid price");
//             return;
//         }
        
//         Console.Write("Enter SKU: ");
//         var sku = Console.ReadLine() ?? string.Empty;
        
//         Console.Write("Enter category ID: ");
//         if (!int.TryParse(Console.ReadLine(), out var categoryId))
//         {
//             Console.WriteLine("Invalid category ID");
//             return;
//         }
        
//         var product = new Product
//         {
//             Name = name,
//             Description = description,
//             Price = price,
//             SKU = sku,
//             CategoryId = categoryId
//         };
        
//         db.Products.Add(product);
//         db.SaveChanges();
        
//         // Add inventory record
//         Console.Write("Enter initial stock quantity: ");
//         if (!int.TryParse(Console.ReadLine(), out var quantity))
//         {
//             Console.WriteLine("Invalid quantity");
//             return;
//         }
        
//         var inventory = new Inventory
//         {
//             ProductId = product.ProductId,
//             QuantityInStock = quantity,
//             LastRestocked = DateTime.Now,
//             MinimumStockLevel = 5
//         };
        
//         db.Inventories.Add(inventory);
//         db.SaveChanges();
        
//         Console.WriteLine("Product and inventory added successfully!");
//     }

//     static void ViewInventory(RetailDbContext db)
//     {
//         var products = db.Products
//             .Include(p => p.Category)
//             .Include(p => p.Inventory)
//             .ToList();
        
//         Console.WriteLine("\nCurrent Inventory:");
//         Console.WriteLine("ID\tName\t\tCategory\tPrice\tStock");
        
//         foreach (var product in products)
//         {
//             Console.WriteLine($"{product.ProductId}\t{product.Name}\t{product.Category?.Name}\t{product.Price:C}\t{product.Inventory?.QuantityInStock}");
//         }
//     }
// }
using Microsoft.EntityFrameworkCore;
using RetailInventory.Data;
using RetailInventory.Models;

class Program
{
    static async Task Main(string[] args)
    {
        using var db = new RetailDbContext();
        db.Database.EnsureCreated();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Retail Inventory Management System");
            Console.WriteLine("1. Add Category");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. View Inventory");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await AddCategory(db);
                    break;
                case "2":
                    await AddProduct(db);
                    break;
                case "3":
                    await ViewInventory(db);
                    break;
                case "4":
                    await UpdateProduct(db);
                    break;
                case "5":
                    await DeleteProduct(db);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static async Task AddCategory(RetailDbContext db)
    {
        Console.Write("Enter category name: ");
        var name = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter category description: ");
        var description = Console.ReadLine() ?? string.Empty;

        var category = new Category
        {
            Name = name,
            Description = description
        };

        db.Categories.Add(category);
        await db.SaveChangesAsync();
        Console.WriteLine("Category added successfully! Press any key to continue...");
        Console.ReadKey();
    }

    static async Task AddProduct(RetailDbContext db)
    {
        Console.Write("Enter product name: ");
        var name = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter product description: ");
        var description = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter product price: ");
        if (!decimal.TryParse(Console.ReadLine(), out var price))
        {
            Console.WriteLine("Invalid price");
            Console.ReadKey();
            return;
        }

        Console.Write("Enter SKU: ");
        var sku = Console.ReadLine() ?? string.Empty;

        // Show available categories
        var categories = await db.Categories.ToListAsync();
        Console.WriteLine("\nAvailable Categories:");
        foreach (var cat in categories)
        {
            Console.WriteLine($"{cat.CategoryId}. {cat.Name}");
        }

        Console.Write("Enter category ID: ");
        if (!int.TryParse(Console.ReadLine(), out var categoryId) || 
            !categories.Any(c => c.CategoryId == categoryId))
        {
            Console.WriteLine("Invalid category ID");
            Console.ReadKey();
            return;
        }

        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price,
            SKU = sku,
            CategoryId = categoryId
        };

        db.Products.Add(product);
        await db.SaveChangesAsync();

        // Add inventory record
        Console.Write("Enter initial stock quantity: ");
        if (!int.TryParse(Console.ReadLine(), out var quantity))
        {
            Console.WriteLine("Invalid quantity");
            Console.ReadKey();
            return;
        }

        var inventory = new Inventory
        {
            ProductId = product.ProductId,
            QuantityInStock = quantity,
            LastRestocked = DateTime.Now,
            MinimumStockLevel = 5
        };

        db.Inventories.Add(inventory);
        await db.SaveChangesAsync();

        Console.WriteLine("Product and inventory added successfully! Press any key to continue...");
        Console.ReadKey();
    }

    static async Task ViewInventory(RetailDbContext db)
    {
        var products = await db.Products
            .Include(p => p.Category)
            .Include(p => p.Inventory)
            .ToListAsync();

        Console.WriteLine("\nCurrent Inventory:");
        Console.WriteLine("ID\tName\t\tCategory\tPrice\tStock");
        
        foreach (var product in products)
        {
            Console.WriteLine($"{product.ProductId}\t{product.Name}\t{product.Category?.Name}\t{product.Price:C}\t{product.Inventory?.QuantityInStock}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static async Task UpdateProduct(RetailDbContext db)
    {
        Console.Write("Enter product ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out var productId))
        {
            Console.WriteLine("Invalid product ID");
            Console.ReadKey();
            return;
        }

        var product = await db.Products
            .Include(p => p.Category)
            .Include(p => p.Inventory)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
        {
            Console.WriteLine("Product not found");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"\nCurrent Details:");
        Console.WriteLine($"Name: {product.Name}");
        Console.WriteLine($"Description: {product.Description}");
        Console.WriteLine($"Price: {product.Price:C}");
        Console.WriteLine($"SKU: {product.SKU}");
        Console.WriteLine($"Category: {product.Category?.Name}");
        Console.WriteLine($"Stock: {product.Inventory?.QuantityInStock}");

        Console.WriteLine("\nEnter new values (leave blank to keep current):");

        Console.Write($"Name [{product.Name}]: ");
        var name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
            product.Name = name;

        Console.Write($"Description [{product.Description}]: ");
        var description = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(description))
            product.Description = description;

        Console.Write($"Price [{product.Price}]: ");
        var priceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out var newPrice))
            product.Price = newPrice;

        Console.Write($"SKU [{product.SKU}]: ");
        var sku = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(sku))
            product.SKU = sku;

        // Update inventory if exists
        if (product.Inventory != null)
        {
            Console.Write($"Stock Quantity [{product.Inventory.QuantityInStock}]: ");
            var quantityInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(quantityInput) && int.TryParse(quantityInput, out var newQuantity))
                product.Inventory.QuantityInStock = newQuantity;
        }

        await db.SaveChangesAsync();
        Console.WriteLine("Product updated successfully! Press any key to continue...");
        Console.ReadKey();
    }

    static async Task DeleteProduct(RetailDbContext db)
    {
        Console.Write("Enter product ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out var productId))
        {
            Console.WriteLine("Invalid product ID");
            Console.ReadKey();
            return;
        }

        var product = await db.Products
            .Include(p => p.Inventory)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
        {
            Console.WriteLine("Product not found");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"\nProduct to delete:");
        Console.WriteLine($"Name: {product.Name}");
        Console.WriteLine($"Price: {product.Price:C}");
        Console.WriteLine($"Stock: {product.Inventory?.QuantityInStock}");

        Console.Write("\nAre you sure you want to delete this product? (y/n): ");
        var confirm = Console.ReadLine()?.ToLower();

        if (confirm == "y")
        {
            if (product.Inventory != null)
                db.Inventories.Remove(product.Inventory);
            
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            Console.WriteLine("Product deleted successfully!");
        }
        else
        {
            Console.WriteLine("Deletion cancelled.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}