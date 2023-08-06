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
    public class CartsCollectionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CartsCollectionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Carts
        [HttpGet("GetUserCartItems")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetUserCartItems(UserDTO userDTO)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            IEnumerable<Cart> source = await _context.Carts.Where(cart=>cart.UserId == userDTO.UserId).ToListAsync();
            IEnumerable<CartDTO> result = _mapper.Map<IEnumerable<CartDTO>>(source);

            return Ok(result);
        }
    }
}
