using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Entities;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsCollectionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsCollectionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        // GET: api/ProductsCollections
        [HttpGet("GetCategoryProducts/{productCategoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(int productCategoryId)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            IEnumerable<Product> source = await _context.Products.Where(Products => Products.CategoryId == productCategoryId).ToListAsync();
            IEnumerable<ProductDTO> result = _mapper.Map<IEnumerable<ProductDTO>>(source);

            return Ok(result);
        }
    }
}
