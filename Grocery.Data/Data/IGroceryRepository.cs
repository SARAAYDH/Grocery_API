namespace Grocery.Data;
using Grocery.Data.Models;
public interface IGroceryRepository
{
    public Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);

    public Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);

    public Task AddProductAsync(Product product, CancellationToken cancellationToken);

    public Task EditProductAsync(Product product, CancellationToken cancellationToken);

    public Task DeleteProductAsync(int id, CancellationToken cancellationToken);
}
