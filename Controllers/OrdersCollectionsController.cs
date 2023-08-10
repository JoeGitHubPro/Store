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
    public class OrdersCollectionsController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrdersCollectionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


        // GET: api/OrdersCollections
        [HttpGet("GetUserOrders")]
        public async Task<ActionResult<IEnumerable<OrderViewDTO>>> GetOrders(UserDTO userDTO)
        {
            if (_context.OrderView == null)
            {
                return NotFound();
            }

            IEnumerable<OrderView> source = await _context.OrderView.AsNoTracking().Where(a=>a.UserName == userDTO.UserName).ToListAsync();
           
            
            IEnumerable<OrderViewDTO> result = _mapper.Map<IEnumerable<OrderViewDTO>>(source);

            return Ok(result);
        }
    }
}
