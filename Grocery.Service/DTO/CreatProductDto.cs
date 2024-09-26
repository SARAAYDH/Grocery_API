namespace Grocery.Service.DTO;
using System.ComponentModel.DataAnnotations;
public class CreatProductDto
{
    [Required]
    [MaxLength(100)]
    [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
    public string? Name { get; set; }

    [Required]
    [Range(2, 300, ErrorMessage = "Price must be between 2 and 300")]
    public decimal Price { get; set; }

    [Required]
    public bool IsAvailable { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
