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
    [StringLength(100)]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 10000)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
}