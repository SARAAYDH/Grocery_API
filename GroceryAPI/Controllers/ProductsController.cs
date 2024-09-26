using Grocery.Service;
using Grocery.Service.DTO;
using Microsoft.AspNetCore.Mvc;
namespace GroceryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IGroceryService _service;

    public ProductsController(IGroceryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<RequestedProduct>> GetAllProudct()
    {
        var productsList = await _service.GetAllAsync(CancellationToken.None);
        return productsList ?? new List<RequestedProduct>();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RequestedProduct>> GetProudct(int Id)
    {
        var product = await _service.GetByIdAsync(Id, CancellationToken.None);
        if (product == null)
        {
            return NotFound("No product found with this Id");
        }

        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditProduct(int id, [FromBody] RequestedProduct product, CancellationToken cancellationToken)
    {
       if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

       var existingProduct = await _service.GetByIdAsync(id, cancellationToken);
       if (existingProduct == null)
        {
            return NotFound($"Product with ID {product.Id} not found.");
        }

       var productToEdit = new EditProduct
        {
            Id = product.Id,
            Name = product.Name,
            IsAvailable = product.IsAvailable,
            Price = product.Price,
            CategoryId = product.CategoryId,
        };
       await _service.EditAsync(productToEdit, cancellationToken);

       return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(CreatProductDto product, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid || product == null)
        {
            return BadRequest(product == null ? "Product data is required." : ModelState);
        }

        await _service.AddProductAsync(product, cancellationToken);
        return CreatedAtAction(nameof(GetAllProudct), product);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id, CancellationToken cancellationToken)
    {
        var product = await _service.GetByIdAsync(id, cancellationToken);
        if (product == null)
        {
            return NotFound($"Product with Id {id} not found.");
        }

        await _service.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}
