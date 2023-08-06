using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Entities;
using Store.Extensions;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
           
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            IEnumerable<Product> source = await _context.Products.ToListAsync();
            IEnumerable<ProductDTO> result = _mapper.Map<IEnumerable<ProductDTO>>(source);

            return Ok(result);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            Product? product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDTO result = _mapper.Map<ProductDTO>(product);

            return Ok(result);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromForm]ProductDTO productDTO, [FromForm] IFormFile? image)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }

            Product product = _mapper.Map<Product>(productDTO);

            if (image is not null)
            {

                Product? dataProduct =  _context.Products.Where(x=>x.Id == id).AsNoTracking().FirstOrDefault();
                await FileUpload.Delete(dataProduct.Image!);
                product.Image = await FileUpload.Upload(image);
            }



            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct([FromForm]IFormFile image,[FromForm] ProductDTO productDTO)
        {
          
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products'  is null.");
            }
            Product product = _mapper.Map<Product>(productDTO);

            try
            {
                product.Image = await FileUpload.Upload(image);
            }
            catch (Exception)
            {

                return BadRequest("Image upload fail");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, _mapper.Map<ProductDTO>(product));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await FileUpload.Delete(product.Image!);

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
