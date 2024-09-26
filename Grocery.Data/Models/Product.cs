using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Grocery.Data.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    [Range(2, 300)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public bool IsAvailable { get; set; } = true;

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
   
    [JsonIgnore]
    public virtual Category? Category { get; set; }
}
