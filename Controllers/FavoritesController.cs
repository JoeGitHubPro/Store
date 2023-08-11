using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Core.UnitWork;
using Store.EF.Data;
using Store.Core.Entities;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavoritesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteDTO>>> GetFavorites()
        {
            if (_unitOfWork.Favorites == null)
                return NotFound();

            IEnumerable<FavoriteDTO> result = _mapper.Map<IEnumerable<FavoriteDTO>>(await _unitOfWork.Favorites.Get());

            return Ok(result);
        }

        // GET: api/Favorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteDTO>> GetFavorite(int id)
        {
            if (_unitOfWork.Favorites == null)
                return NotFound();


            Favorite? favorite = await _unitOfWork.Favorites.Get(id);

            if (favorite == null)
                return NotFound();


            FavoriteDTO result = _mapper.Map<FavoriteDTO>(favorite);

            return result;
        }

        // PUT: api/Favorites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorite(int id, FavoriteDTO favoriteDTO)
        {

            if (id != favoriteDTO.Id)
                return BadRequest();

            Favorite favorite = _mapper.Map<Favorite>(favoriteDTO);

            await _unitOfWork.Favorites.Put(id, favorite);

            await _unitOfWork.Commit();

            return NoContent();
        }

        // POST: api/Favorites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavoriteDTO>> PostFavorite(FavoriteDTO favoriteDTO)
        {
            if (_unitOfWork.Favorites == null)
                return Problem("Entity set 'AppDbContext.Favorites'  is null.");


            Favorite favorite = _mapper.Map<Favorite>(favoriteDTO);

            await _unitOfWork.Favorites.Post(favorite);

            await _unitOfWork.Commit();

            return CreatedAtAction("GetFavorite", new { id = favorite.Id }, _mapper.Map<FavoriteDTO>(favorite));
        }

        // DELETE: api/Favorites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            if (_unitOfWork.Favorites == null)
                return NotFound();

            await _unitOfWork.Favorites.Delete(id);

            await _unitOfWork.Commit();

            return NoContent();
        }

        //private bool FavoriteExists(int id)
        //{
        //    return (_unitOfWork.Favorites?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
