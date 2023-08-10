using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Entities;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FavoritesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteDTO>>> GetFavorites()
        {
            if (_context.Favorites == null)
            {
                return NotFound();
            }

            IEnumerable<Favorite> source = await _context.Favorites.AsNoTracking().ToListAsync();
            IEnumerable<FavoriteDTO> result = _mapper.Map<IEnumerable<FavoriteDTO>>(source);

            return Ok(result);
        }

        // GET: api/Favorites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavoriteDTO>> GetFavorite(int id)
        {
            if (_context.Favorites == null)
            {
                return NotFound();
            }

            Favorite? favorite = await _context.Favorites.FindAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            FavoriteDTO result = _mapper.Map<FavoriteDTO>(favorite);

            return result;
        }

        // PUT: api/Favorites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorite(int id, FavoriteDTO favoriteDTO)
        {
            
            if (id != favoriteDTO.Id)
            {
                return BadRequest();
            }
            Favorite favorite = _mapper.Map<Favorite>(favoriteDTO);
            _context.Entry(favorite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteExists(id))
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

        // POST: api/Favorites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavoriteDTO>> PostFavorite(FavoriteDTO favoriteDTO)
        {
            if (_context.Favorites == null)
            {
                return Problem("Entity set 'AppDbContext.Favorites'  is null.");
            }

            Favorite favorite = _mapper.Map<Favorite>(favoriteDTO);

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavorite", new { id = favorite.Id }, _mapper.Map<FavoriteDTO>(favorite));
        }

        // DELETE: api/Favorites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            if (_context.Favorites == null)
            {
                return NotFound();
            }
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavoriteExists(int id)
        {
            return (_context.Favorites?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
