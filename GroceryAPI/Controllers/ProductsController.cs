using GroceryAPI.Data;
using GroceryAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private GroceryContext _context;
        public ProductsController(GroceryContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllProudct()
        {
            return await _context.Products.ToArrayAsync();

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> EditProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();

            }
            else if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(i => i.Id == id)) { return NotFound(); }
                    else { throw; }
                }
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            if (ModelState.IsValid) {

                _context.Add(product);
               await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAllProudct), new {id =product.Id},product);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product= _context.Products.First(p => p.Id == id);
            if (product==null)
            {
                return NotFound();

            }
            else
            {
                try
                {
              
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(i => i.Id == id)) { return NotFound(); }

                    else { throw; }
                }
            }
            return NoContent();


        }
    }
}
