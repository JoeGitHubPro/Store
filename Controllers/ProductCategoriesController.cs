using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Elfie.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Entities;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductCategoriesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategories()
        {
            if (_context.ProductCategories == null)
            {
                return NotFound();
            }
            IEnumerable<ProductCategory> source = await _context.ProductCategories.ToListAsync();
            IEnumerable<ProductCategoryDTO> result = _mapper.Map<IEnumerable<ProductCategoryDTO>>(source);
            return Ok(result);
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id?}")]
        public async Task<ActionResult<ProductCategoryDTO>> GetProductCategory(int id)
        {
            if (_context.ProductCategories == null)
            {
                return NotFound();
            }
            ProductCategory? productCategory = await _context.ProductCategories.FindAsync(id);

            if (productCategory == null)
            {
                return NotFound();
            }
            ProductCategoryDTO result = _mapper.Map<ProductCategoryDTO>(productCategory);

            return Ok(result);
        }

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategoryDTO productCategoryDTO)
        {
            if (id != productCategoryDTO.Id)
            {
                return BadRequest();
            }

            ProductCategory productCategory = _mapper.Map<ProductCategory>(productCategoryDTO);
            _context.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST: api/ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDTO>> PostProductCategory(ProductCategoryDTO productCategoryDTO)
        {
            if (_context.ProductCategories == null)
            {
                return Problem("Entity set 'AppDbContext.ProductCategories'  is null.");
            }

            ProductCategory productCategory = _mapper.Map<ProductCategory>(productCategoryDTO);
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductCategory", new { id = productCategory.Id }, _mapper.Map<ProductCategoryDTO>(productCategory));
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            if (_context.ProductCategories == null)
            {
                return NotFound();
            }
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCategoryExists(int id)
        {
            return (_context.ProductCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
