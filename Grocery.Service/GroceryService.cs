using Grocery.Data;
using Grocery.Data.Models;
using Grocery.Service.DTO;
namespace Grocery.Service;

public class GroceryService : IGroceryService
{
    private readonly IGroceryRepository _repository;
    public GroceryService(IGroceryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RequestedProduct>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllProductsAsync(cancellationToken);
        List<RequestedProduct> requestedproducts = products.Select(product => new RequestedProduct
        {
            Id = product.Id,
            Name = product.Name,
            IsAvailable = product.IsAvailable,
            Price = product.Price,
            CategoryId = product.CategoryId
        }).ToList();
        return requestedproducts;
    }

    public async Task<Product?> GetByIdAsync(int Id, CancellationToken cancellationToken)
    {
        return await _repository.GetProductByIdAsync(Id, cancellationToken);
    }

    public async Task AddProductAsync(CreatProductDto product, CancellationToken cancellationToken)
    {
        Product newProduct = new Product
        {
            Name = product.Name,
            IsAvailable = product.IsAvailable,
            Price = product.Price,
            CategoryId = product.CategoryId,
        };
        await _repository.AddProductAsync(newProduct, cancellationToken);
    }

    public async Task EditAsync(EditProduct product, CancellationToken cancellationToken)
    {
        Product EditedProduct = new Product
        {
            Id = product.Id,
            Name = product.Name,
            IsAvailable = product.IsAvailable,
            Price = product.Price,
            CategoryId = product.CategoryId,
        };
        await _repository.EditProductAsync(EditedProduct, cancellationToken);
    }

    public async Task DeleteAsync(int Id, CancellationToken cancellationToken)
    {
        await _repository.DeleteProductAsync(Id, cancellationToken);
    }
}
