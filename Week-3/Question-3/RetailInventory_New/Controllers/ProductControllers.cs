// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using RetailInventory_New.Data;
// using RetailInventory_New.Models;

// namespace RetailInventory_New.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class ProductsController : ControllerBase
//     {
//         private readonly RetailDbContext _context;

//         public ProductsController(RetailDbContext context)
//         {
//             _context = context;
//         }


//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
//         {
//             return await _context.Products
//                 .Include(p => p.Category)
//                 .ToListAsync();
//         }


//         [HttpGet("{id}")]
//         public async Task<ActionResult<Product>> GetProduct(int id)
//         {
//             var product = await _context.Products
//                 .Include(p => p.Category)
//                 .FirstOrDefaultAsync(p => p.Id == id);

//             if (product == null)
//             {
//                 return NotFound();
//             }

//             return product;
//         }

//         [HttpPost]
//         public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
//         {

//             var category = await _context.Categories.FindAsync(product.CategoryId);
//             if (category == null)
//             {
//                 return BadRequest($"Category with Id {product.CategoryId} does not exist.");
//             }

//             _context.Products.Add(product);
//             await _context.SaveChangesAsync();


//             await _context.Entry(product).Reference(p => p.Category).LoadAsync();

//             return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
//         }


//     }
// }















//lab - 7 

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailInventory_New.Data;
using RetailInventory_New.Models;

namespace RetailInventory_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly RetailDbContext _context;

        public ProductsController(RetailDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            var category = await _context.Categories.FindAsync(product.CategoryId);
            if (category == null)
            {
                return BadRequest($"Category with Id {product.CategoryId} does not exist.");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            await _context.Entry(product).Reference(p => p.Category).LoadAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}


//lab -5


// [HttpGet("dashboard")]
// public async Task<IActionResult> GetDashboardData()
// {
//     var products = await _context.Products.ToListAsync();
//     var product = await _context.Products.FindAsync(1);
//     var expensive = await _context.Products.FirstOrDefaultAsync(p => p.Price > 5000);

//     return Ok(new
//     {
//         AllProducts = products.Select(p => $"{p.Name} - ₹{p.Price}"),
//         FoundProduct = product?.Name,
//         ExpensiveProduct = expensive?.Name
//     });
// }



