using api_gestion_productos.Data;
using api_gestion_productos.Models;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_productos.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        return await _context.Products
            .Where(p => p.isActive)
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
    }

    public async Task<ProductResponseDto?> GetProductByIdAsync(int id)
    {
        var product = await _context.Products
            .Where(p => p.id == id && p.isActive)
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
            .FirstOrDefaultAsync();

        return product;
    }

    public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto productDto)
    {
        var product = new Product
        {
            name = productDto.name,
            description = productDto.description,
            price = productDto.price,
            stock = productDto.stock,
            category = productDto.category,
            createdAt = DateTime.UtcNow,
            isActive = true
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return new ProductResponseDto
        {
            id = product.id,
            name = product.name,
            description = product.description,
            price = product.price,
            stock = product.stock,
            category = product.category,
            createdAt = product.createdAt,
            updatedAt = product.updatedAt,
            isActive = product.isActive
        };
    }

    public async Task<ProductResponseDto?> UpdateProductAsync(int id, UpdateProductDto productDto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null || !product.isActive)
            return null;

        if (productDto.name != null)
            product.name = productDto.name;
        if (productDto.description != null)
            product.description = productDto.description;
        if (productDto.price.HasValue)
            product.price = productDto.price.Value;
        if (productDto.stock.HasValue)
            product.stock = productDto.stock.Value;
        if (productDto.category != null)
            product.category = productDto.category;

        product.updatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new ProductResponseDto
        {
            id = product.id,
            name = product.name,
            description = product.description,
            price = product.price,
            stock = product.stock,
            category = product.category,
            createdAt = product.createdAt,
            updatedAt = product.updatedAt,
            isActive = product.isActive
        };
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null || !product.isActive)
            return false;

        product.isActive = false;
        product.updatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsByCategoryAsync(string category)
    {
        return await _context.Products
            .Where(p => p.category.ToLower() == category.ToLower() && p.isActive)
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
    }

    public async Task<IEnumerable<ProductResponseDto>> SearchProductsAsync(string searchTerm)
    {
        return await _context.Products
            .Where(p => p.isActive && 
                       (p.name.ToLower().Contains(searchTerm.ToLower()) ||
                        p.description.ToLower().Contains(searchTerm.ToLower()) ||
                        p.category.ToLower().Contains(searchTerm.ToLower())))
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
    }

    public async Task<IEnumerable<ProductResponseDto>> GetLowStockProductsAsync(int threshold = 10)
    {
        return await _context.Products
            .Where(p => p.stock <= threshold && p.isActive)
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
    }
}
