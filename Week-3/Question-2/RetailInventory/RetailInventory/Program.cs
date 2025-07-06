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
            Console.WriteLine("6. View Categories");
            Console.WriteLine("7. Run Product Queries");
            Console.WriteLine("8. Exit");
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
                    await ViewCategories(db);
                    break;
                case "7":
                    await QueryProducts(db);
                    break;
                case "8":
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

        db.Categories.Add(new Category { Name = name, Description = description });
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

        Console.WriteLine("\nAvailable Categories:");
        var categories = await db.Categories.ToListAsync();
        foreach (var cat in categories)
        {
            Console.WriteLine($"{cat.Id}. {cat.Name}");
        }

        Console.Write("Enter category ID: ");
        if (!int.TryParse(Console.ReadLine(), out var categoryId))
        {
            Console.WriteLine("Invalid category ID");
            Console.ReadKey();
            return;
        }

        db.Products.Add(new Product 
        { 
            Name = name, 
            Description = description, 
            Price = price, 
            SKU = sku, 
            CategoryId = categoryId 
        });
        await db.SaveChangesAsync();
        Console.WriteLine("Product added successfully! Press any key to continue...");
        Console.ReadKey();
    }

    static async Task ViewInventory(RetailDbContext db)
    {
        var products = await db.Products
            .Include(p => p.Category)
            .ToListAsync();

        Console.WriteLine("\nCurrent Inventory:");
        Console.WriteLine("ID\tName\t\tCategory\tPrice\tStock");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id}\t{product.Name}\t{product.Category?.Name}\t{product.Price:C}");
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

        var product = await db.Products.FindAsync(productId);
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

        Console.WriteLine("\nEnter new values (leave blank to keep current):");
        Console.Write($"Name [{product.Name}]: ");
        var name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name)) product.Name = name;

        Console.Write($"Description [{product.Description}]: ");
        var description = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(description)) product.Description = description;

        Console.Write($"Price [{product.Price}]: ");
        var priceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out var newPrice))
            product.Price = newPrice;

        Console.Write($"SKU [{product.SKU}]: ");
        var sku = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(sku)) product.SKU = sku;

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

        var product = await db.Products.FindAsync(productId);
        if (product == null)
        {
            Console.WriteLine("Product not found");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"\nProduct to delete:");
        Console.WriteLine($"Name: {product.Name}");
        Console.WriteLine($"Price: {product.Price:C}");

        Console.Write("\nAre you sure you want to delete this product? (y/n): ");
        var confirm = Console.ReadLine()?.ToLower();

        if (confirm == "y")
        {
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

    static async Task ViewCategories(RetailDbContext db)
    {
        var categories = await db.Categories.ToListAsync();
        Console.WriteLine("\nCategories:");
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Id}. {category.Name} - {category.Description}");
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static async Task QueryProducts(RetailDbContext db)
    {
        // 1. Filter expensive products and sort by price
        Console.WriteLine("\nExpensive Products (Price > 1000):");
        var expensiveProducts = await db.Products
            .Include(p => p.Category)
            .Where(p => p.Price > 1000)
            .OrderByDescending(p => p.Price)
            .ToListAsync();

        foreach (var product in expensiveProducts)
        {
            Console.WriteLine($"{product.Name} - {product.Price:C} ({product.Category?.Name})");
        }

        // 2. Project into DTO
        Console.WriteLine("\nProduct Price List:");
        var productDTOs = await db.Products
            .Include(p => p.Category)
            .Select(p => new ProductPriceDTO 
            { 
                Name = p.Name, 
                Price = p.Price,
                Category = p.Category.Name 
            })
            .OrderBy(p => p.Name)
            .ToListAsync();

        foreach (var dto in productDTOs)
        {
            Console.WriteLine($"{dto.Name}: {dto.Price:C} ({dto.Category})");
        }

        // 3. Group products by category
        Console.WriteLine("\nProducts by Category:");
        var productsByCategory = await db.Categories
            .Include(c => c.Products)
            .Select(c => new CategoryProductsDTO
            {
                CategoryName = c.Name,
                ProductCount = c.Products.Count,
                AveragePrice = c.Products.Average(p => p.Price)
            })
            .ToListAsync();

        foreach (var group in productsByCategory)
        {
            Console.WriteLine($"{group.CategoryName}: {group.ProductCount} products, avg price {group.AveragePrice:C}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}