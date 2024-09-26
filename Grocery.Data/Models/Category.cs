using System.ComponentModel.DataAnnotations;
namespace Grocery.Data.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public virtual List<Product>? Products { get; set; }
}