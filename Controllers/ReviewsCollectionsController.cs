using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Entities;
using System.Linq;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsCollectionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ReviewsCollectionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: api/ReviewsCollections
        [HttpGet("GetProductReviews/{productId}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetProductReviews(int productId)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            IEnumerable<Review> source = await _context.Reviews.AsNoTracking().Where(Reviews => Reviews.ProductId == productId).ToListAsync();

            IEnumerable<ReviewDTO> result = _mapper.Map<IEnumerable<ReviewDTO>>(source);

            return Ok(result);
        }

    }
}
