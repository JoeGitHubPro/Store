using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.EF.Data;
using Store.Core.Entities;
using Store.Core.UnitWork;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetCarts()
        {
          if (_unitOfWork.Carts == null)
                        return NotFound();
          
            IEnumerable<CartDTO> result = _mapper.Map<IEnumerable<CartDTO>>(await _unitOfWork.Carts.Get());

            return Ok(result);
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetCart(int id)
        {
          if (_unitOfWork.Carts == null)
              return NotFound();
          
         
            Cart? cart = await _unitOfWork.Carts.Get(id);

            if (cart == null)
                            return NotFound();
            
            CartDTO result = _mapper.Map<CartDTO>(cart);

            return result;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, CartDTO cartDTO)
        {
            if (id != cartDTO.Id)
            {
                return BadRequest();
            }

            Cart cart = _mapper.Map<Cart>(cartDTO);

            await _unitOfWork.Carts.Put(id ,cart);

            await _unitOfWork.Commit();

            return NoContent();
        }

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartDTO>> PostCart(CartDTO cartDTO)
        {
          if (_unitOfWork.Carts == null)
                        return Problem("Entity set 'AppDbContext.Carts'  is null.");
          
            Cart cart = _mapper.Map<Cart>(cartDTO);

            await _unitOfWork.Carts.Post(cart);

            await _unitOfWork.Commit();

            return CreatedAtAction("GetCart", new { id = cart.Id }, _mapper.Map<CartDTO>(cart));
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_unitOfWork.Carts == null)
                return NotFound();
            
            await _unitOfWork.Carts.Delete(id);

            await _unitOfWork.Commit();

            return NoContent();
        }

        //private bool CartExists(int id)
        //{
        //    return (_unitOfWork.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
