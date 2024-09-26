using Grocery.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace Grocery.Data;

public class GroceryRepository : IGroceryRepository
{
    private GroceryDbContext _context;
    public GroceryRepository(GroceryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task EditProductAsync(Product product, CancellationToken cancellationToken)
    {
        var productToUpdate = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id, cancellationToken: cancellationToken);
        if(productToUpdate != null) 
        { 
        productToUpdate.Name = product.Name;
        productToUpdate.Price = product.Price;
        await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        var productToDelete = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (productToDelete != null)
        {
            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
