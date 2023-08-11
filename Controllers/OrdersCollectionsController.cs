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
    public class OrdersCollectionsController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersCollectionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }


        // GET: api/OrdersCollections
        [HttpGet("GetUserOrders")]
        public async Task<ActionResult<IEnumerable<OrderViewDTO>>> GetOrders(UserDTO userDTO)
        {
            if (_unitOfWork.OrderView is null)
                           return NotFound();
            

            IEnumerable<OrderView> source = await _unitOfWork.OrderView.Get(a=>a.UserName == userDTO.UserName);
           
            
            IEnumerable<OrderViewDTO> result = _mapper.Map<IEnumerable<OrderViewDTO>>(source);

            return Ok(result);
        }
    }
}
