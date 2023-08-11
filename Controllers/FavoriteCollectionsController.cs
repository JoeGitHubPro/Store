using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.EF.Data;
using Store.Core.DOTs;
using Store.Core.Entities;
using Store.Core.UnitWork;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteCollectionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FavoriteCollectionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/FavoriteCollections/GetUserFavorites
        [HttpGet("GetUserFavorites")]
        public async Task<ActionResult<IEnumerable<FavoriteDTO>>> GetUserFavorites(UserDTO userDTO)
        {
            if (_unitOfWork.Favorites == null)
                return NotFound();
            

            IEnumerable<Favorite> source = await _unitOfWork.Favorites.Get(favorite=> favorite.UserId == userDTO.UserId);

            IEnumerable<FavoriteDTO> result = _mapper.Map<IEnumerable<FavoriteDTO>>(source);

            return Ok(result);
        }
    }
}
