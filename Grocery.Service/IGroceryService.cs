using Grocery.Data.Models;
using Grocery.Service.DTO;
namespace Grocery.Service;

public interface IGroceryService
{
    public Task<List<RequestedProduct>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Product?> GetByIdAsync(int Id, CancellationToken cancellationToken);
    public Task AddProductAsync(CreatProductDto product, CancellationToken cancellationToken);
    public Task EditAsync(EditProduct product, CancellationToken cancellationToken);
    public Task DeleteAsync(int Id, CancellationToken cancellationToken);
}
