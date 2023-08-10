using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Entities;
using Store.DOTs;
using Elfie.Serialization;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrdersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewDTO>>> GetOrders()
        {
            if (_context.OrderView == null)
            {
                return NotFound();
            }

            IEnumerable<OrderView> source = await _context.OrderView.AsNoTracking().ToListAsync();
            IEnumerable<OrderViewDTO> result = _mapper.Map<IEnumerable<OrderViewDTO>>(source);

            return Ok(result);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewDTO>> GetOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            OrderView? order = await _context.OrderView.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
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


            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderDTO orderDTO)
        {
            if (_context.Orders == null || _context.Carts == null || _context.Products == null)
            {
                return Problem("Entity set 'AppDbContext'  is null.");
            }

            Order order = _mapper.Map<Order>(orderDTO);

            IEnumerable<Cart> cartsSource = await _context.Carts.Where(cart => cart.UserId == order.UserId).ToListAsync();
            IEnumerable<CartDTO> carts = _mapper.Map<IEnumerable<CartDTO>>(cartsSource);
            IEnumerable<OrderDTO> orders = _mapper.Map<IEnumerable<OrderDTO>>(carts);

            foreach (var item in orders)
            {
                item.Address = orderDTO.Address;
                Product? product = await _context.Products.AsNoTracking().Where(a => a.Id == item.ProductId).FirstOrDefaultAsync();
                int? DeliveryTime = product?.DeliveryTime;

                if (DeliveryTime is not null)
                {

                    item.DeliveryDate = DateTime.UtcNow.AddDays(Convert.ToDouble(DeliveryTime));
                }

                item.TotalPrice = product.Price * item.Count;
            }

            IEnumerable<Order> result = _mapper.Map<IEnumerable<Order>>(orders);

            try
            {
                await _context.Orders.AddRangeAsync(result);
                _context.Carts.RemoveRange(cartsSource);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();

            //return CreatedAtAction("GetOrder",, new { id = order.Id }, _mapper.Map < IEnumerable<OrderViewDTO>>(result));
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
