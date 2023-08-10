using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.DOTs;
using Store.Entities;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteCollectionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FavoriteCollectionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/FavoriteCollections/GetUserFavorites
        [HttpGet("GetUserFavorites")]
        public async Task<ActionResult<IEnumerable<FavoriteDTO>>> GetUserFavorites(UserDTO userDTO)
        {
            if (_context.Favorites == null)
            {
                return NotFound();
            }

            IEnumerable<Favorite> source = await _context.Favorites.AsNoTracking().Where(favorite=> favorite.UserId == userDTO.UserId).ToListAsync();
            IEnumerable<FavoriteDTO> result = _mapper.Map<IEnumerable<FavoriteDTO>>(source);

            return Ok(result);
        }
    }
}
