using System.ComponentModel.DataAnnotations;

namespace api_gestion_productos.Models;

public class Product
{
    public int id { get; set; }
    
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
    
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    public DateTime? updatedAt { get; set; }
    public bool isActive { get; set; } = true;
}
