using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.EF.Data;
using Store.Core.Entities;
using Store.Core.DOTs;
using Elfie.Serialization;
using Store.Core.UnitWork;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewDTO>>> GetOrders()
        {
            if (_unitOfWork.OrderView is null)
                return NotFound();

            IEnumerable<OrderViewDTO> result =
                _mapper.Map<IEnumerable<OrderViewDTO>>(
                    await _unitOfWork.OrderView.Get()
                    );

            return Ok(result);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewDTO>> GetOrder(int id)
        {
            if (_unitOfWork.Orders is null)
                return NotFound();

            OrderView? order = await _unitOfWork.OrderView.Get(id);
            if (order is null)
                return NotFound();

            OrderViewDTO result = _mapper.Map<OrderViewDTO>(order);

            return result;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
                return BadRequest();

            Order? order = _mapper.Map<Order>(orderDTO);

            await _unitOfWork.Orders.Put(id,order);

            await _unitOfWork.Commit();

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderDTO orderDTO)
        {
            if (_unitOfWork.Orders == null || _unitOfWork.Carts == null || _unitOfWork.Products == null)
            {
                return Problem("Entity set 'AppDbContext'  is null.");
            }

            Order order = _mapper.Map<Order>(orderDTO);

            IEnumerable<Cart> cartsSource = await _unitOfWork.Carts.Get(cart => cart.UserId == order.UserId);

            IEnumerable<OrderDTO> orders = _mapper.Map<IEnumerable<OrderDTO>>(_mapper.Map<IEnumerable<CartDTO>>(cartsSource));

            foreach (var item in orders)
            {
                item.Address = orderDTO.Address;
                Product? product = await _unitOfWork.Products.GetSingle(a => a.Id == item.ProductId);
                int? DeliveryTime = product?.DeliveryTime;

                if (DeliveryTime is not null)
                    item.DeliveryDate = DateTime.UtcNow.AddDays(Convert.ToDouble(DeliveryTime));
                
                item.TotalPrice = product.Price * item.Count;
            }

            IEnumerable<Order> result = _mapper.Map<IEnumerable<Order>>(orders);

            await _unitOfWork.Commit();

            return Ok();

            //return CreatedAtAction("GetOrder",, new { id = order.Id }, _mapper.Map < IEnumerable<OrderViewDTO>>(result));
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_unitOfWork.Orders is null)
                           return NotFound();
            
            Order? order = await _unitOfWork.Orders.Delete(id);

            if (order is null)
                            return NotFound();

            await _unitOfWork.Commit();

            return NoContent();
        }

        //private bool OrderExists(int id)
        //{
        //    return (_unitOfWork.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
