using System.ComponentModel.DataAnnotations;

namespace api_gestion_productos.Models;

// DTO para crear un producto
public class CreateProductDto
{
    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string name { get; set; } = "";
    
    [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
    public string description { get; set; } = "";
    
    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal price { get; set; }
    
    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
    public int stock { get; set; }
    
    [Required(ErrorMessage = "La categoría es obligatoria")]
    [StringLength(50, ErrorMessage = "La categoría no puede tener más de 50 caracteres")]
    public string category { get; set; } = "";
}

// DTO para actualizar un producto
public class UpdateProductDto
{
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string? name { get; set; }
    
    [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
    public string? description { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal? price { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
    public int? stock { get; set; }
    
    [StringLength(50, ErrorMessage = "La categoría no puede tener más de 50 caracteres")]
    public string? category { get; set; }
}

// DTO para respuesta de producto
public class ProductResponseDto
{
    public int id { get; set; }
    public string name { get; set; } = "";
    public string description { get; set; } = "";
    public decimal price { get; set; }
    public int stock { get; set; }
    public string category { get; set; } = "";
    public DateTime createdAt { get; set; }
    public DateTime? updatedAt { get; set; }
    public bool isActive { get; set; }
}
