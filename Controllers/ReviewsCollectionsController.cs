using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.EF.Data;
using Store.Core.Entities;
using System.Linq;
using Store.Core.UnitWork;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsCollectionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewsCollectionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        // GET: api/ReviewsCollections
        [HttpGet("GetProductReviews/{productId}")]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetProductReviews(int productId)
        {
            if (_unitOfWork.Reviews == null)
            {
                return NotFound();
            }
            IEnumerable<Review> source = await _unitOfWork.Reviews.Get(Reviews => Reviews.ProductId == productId);
            IEnumerable<ReviewDTO> result = _mapper.Map<IEnumerable<ReviewDTO>>(source);

            return Ok(result);
        }

    }
}
