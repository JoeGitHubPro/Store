using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.UnitWork;
using Store.EF.Data;
using Store.Core.DOTs;
using Store.Core.Entities;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsCollectionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartsCollectionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Carts
        [HttpGet("GetUserCartItems")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetUserCartItems(UserDTO userDTO)
        {
            if (_unitOfWork.Carts == null)
                return NotFound();

            IEnumerable<Cart> source = await _unitOfWork.Carts.Get(cart => cart.UserId == userDTO.UserId);

            IEnumerable<CartDTO> result = _mapper.Map<IEnumerable<CartDTO>>(source);

            return Ok(result);
        }
    }
}
