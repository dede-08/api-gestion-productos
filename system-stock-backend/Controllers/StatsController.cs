using Microsoft.AspNetCore.Mvc;
using api_gestion_productos.Data;
using api_gestion_productos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_productos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene estadísticas generales de productos
        /// </summary>
        [HttpGet("products")]
        public async Task<ActionResult<object>> GetProductStats()
        {
            var totalProducts = await _context.Products.Where(p => p.isActive).CountAsync();
            var lowStockProducts = await _context.Products.Where(p => p.stock <= 10 && p.isActive).CountAsync();
            var outOfStockProducts = await _context.Products.Where(p => p.stock == 0 && p.isActive).CountAsync();
            var totalValue = await _context.Products.Where(p => p.isActive).SumAsync(p => p.price * p.stock);
            var avgPrice = await _context.Products.Where(p => p.isActive).AverageAsync(p => p.price);

            return Ok(new
            {
                totalProducts,
                lowStockProducts,
                outOfStockProducts,
                totalValue = Math.Round(totalValue, 2),
                averagePrice = Math.Round(avgPrice, 2)
            });
        }

        /// <summary>
        /// Obtiene productos por categoría con conteo
        /// </summary>
        [HttpGet("products/by-category")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductsByCategory()
        {
            var categoryStats = await _context.Products
                .Where(p => p.isActive)
                .GroupBy(p => p.category)
                .Select(g => new
                {
                    category = g.Key,
                    count = g.Count(),
                    totalValue = Math.Round(g.Sum(p => p.price * p.stock), 2),
                    averagePrice = Math.Round(g.Average(p => p.price), 2)
                })
                .ToListAsync();

            return Ok(categoryStats);
        }

        /// <summary>
        /// Obtiene los productos más caros
        /// </summary>
        [HttpGet("products/most-expensive")]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetMostExpensiveProducts([FromQuery] int limit = 5)
        {
            var products = await _context.Products
                .Where(p => p.isActive)
                .OrderByDescending(p => p.price)
                .Take(limit)
                .Select(p => new ProductResponseDto
                {
                    id = p.id,
                    name = p.name,
                    description = p.description,
                    price = p.price,
                    stock = p.stock,
                    category = p.category,
                    createdAt = p.createdAt,
                    updatedAt = p.updatedAt,
                    isActive = p.isActive
                })
                .ToListAsync();

            return Ok(products);
        }

        /// <summary>
        /// Obtiene los productos con más stock
        /// </summary>
        [HttpGet("products/highest-stock")]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetHighestStockProducts([FromQuery] int limit = 5)
        {
            var products = await _context.Products
                .Where(p => p.isActive)
                .OrderByDescending(p => p.stock)
                .Take(limit)
                .Select(p => new ProductResponseDto
                {
                    id = p.id,
                    name = p.name,
                    description = p.description,
                    price = p.price,
                    stock = p.stock,
                    category = p.category,
                    createdAt = p.createdAt,
                    updatedAt = p.updatedAt,
                    isActive = p.isActive
                })
                .ToListAsync();

            return Ok(products);
        }

        /// <summary>
        /// Obtiene productos agregados recientemente
        /// </summary>
        [HttpGet("products/recent")]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetRecentProducts([FromQuery] int limit = 10)
        {
            var products = await _context.Products
                .Where(p => p.isActive)
                .OrderByDescending(p => p.createdAt)
                .Take(limit)
                .Select(p => new ProductResponseDto
                {
                    id = p.id,
                    name = p.name,
                    description = p.description,
                    price = p.price,
                    stock = p.stock,
                    category = p.category,
                    createdAt = p.createdAt,
                    updatedAt = p.updatedAt,
                    isActive = p.isActive
                })
                .ToListAsync();

            return Ok(products);
        }
    }
}
