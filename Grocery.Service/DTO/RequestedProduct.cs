using System.ComponentModel.DataAnnotations;
namespace Grocery.Service.DTO;
public class RequestedProduct
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    public int CategoryId { get; set; }
}
