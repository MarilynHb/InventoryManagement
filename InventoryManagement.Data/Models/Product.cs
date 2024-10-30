using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Data.Models;

[Table("Products")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column(TypeName = "nvarchar(50)")]
    public string Code { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [Column(TypeName = "nvarchar(200)")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }
}
