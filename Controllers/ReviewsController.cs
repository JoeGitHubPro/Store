using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.EF.Data;
using Store.Core.Entities;
using Store.Core.UnitWork;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
        {
            if (_unitOfWork.Reviews is null)
                return NotFound();


            IEnumerable<ReviewDTO> result = _mapper.Map<IEnumerable<ReviewDTO>>(await _unitOfWork.Reviews.Get());

            return Ok(result);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(int id)
        {
            if (_unitOfWork.Reviews is null)
                return NotFound();

            Review? review = await _unitOfWork.Reviews.Get(id);

            if (review is null)
                return NotFound();


            ReviewDTO result = _mapper.Map<ReviewDTO>(review);

            return Ok(result);
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDTO reviewDTO)
        {
            if (id != reviewDTO.Id)
                return BadRequest();


            Review review = _mapper.Map<Review>(reviewDTO);

            await _unitOfWork.Reviews.Put(id, review);

            await _unitOfWork.Commit();

            return NoContent();
        }

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> PostReview(ReviewDTO reviewDTO)
        {
            if (_unitOfWork.Reviews == null)
            {
                return Problem("Entity set 'AppDbContext.Reviews'  is null.");
            }

            Review review = _mapper.Map<Review>(reviewDTO);
            await _unitOfWork.Reviews.Post(review);

            await _unitOfWork.Commit();

            return CreatedAtAction("GetReview", new { id = review.Id }, _mapper.Map<ReviewDTO>(review));
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (_unitOfWork.Reviews == null)
                return NotFound();

            await _unitOfWork.Reviews.Delete(id);

            await _unitOfWork.Commit();

            return NoContent();
        }

        //private bool ReviewExists(int id)
        //{
        //    return (_unitOfWork.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
