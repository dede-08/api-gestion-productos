using api_gestion_productos.Models;

namespace api_gestion_productos.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
    Task<ProductResponseDto?> GetProductByIdAsync(int id);
    Task<ProductResponseDto> CreateProductAsync(CreateProductDto productDto);
    Task<ProductResponseDto?> UpdateProductAsync(int id, UpdateProductDto productDto);
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<ProductResponseDto>> GetProductsByCategoryAsync(string category);
    Task<IEnumerable<ProductResponseDto>> SearchProductsAsync(string searchTerm);
    Task<IEnumerable<ProductResponseDto>> GetLowStockProductsAsync(int threshold = 10);

}
