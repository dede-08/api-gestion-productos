using Microsoft.AspNetCore.Mvc;
using api_gestion_productos.Services;
using api_gestion_productos.Models;
using Microsoft.AspNetCore.Authorization;

namespace api_gestion_productos.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        ///obtiene los productos activos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        ///obtiene un producto por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) 
                return NotFound(new { message = "Producto no encontrado" });
            
            return Ok(product);
        }

        ///crear un nuevo producto
        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> Create([FromBody] CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = product.id }, product);
        }


        ///actualiza un producto existente
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponseDto>> Update(int id, [FromBody] UpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.UpdateProductAsync(id, productDto);
            if (product == null)
                return NotFound(new { message = "Producto no encontrado" });

            return Ok(product);
        }

        ///elimina un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
                return NotFound(new { message = "Producto no encontrado" });

            return NoContent();
        }

        ///busca productos por categoria
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetByCategory(string category)
        {
            var products = await _productService.GetProductsByCategoryAsync(category);
            return Ok(products);
        }

        ///busca productos por termino de busqueda
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest(new { message = "El término de búsqueda es requerido" });

            var products = await _productService.SearchProductsAsync(q);
            return Ok(products);
        }

        ///obtiene productos con stock bajo
        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetLowStock([FromQuery] int threshold = 10)
        {
            var products = await _productService.GetLowStockProductsAsync(threshold);
            return Ok(products);
        }
    }
}
