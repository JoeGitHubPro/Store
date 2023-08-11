using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Core.UnitWork;
using Store.EF.Data;
using Store.Core.Entities;
using Store.Extensions;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            if (_unitOfWork.Products == null)
                return NotFound();

            IEnumerable<ProductDTO> result = _mapper.Map<IEnumerable<ProductDTO>>(await _unitOfWork.Products.Get());

            return Ok(result);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            if (_unitOfWork.Products == null)

                return NotFound();

            Product? product = await _unitOfWork.Products.Get(id);

            if (product == null)
                return NotFound();

            ProductDTO result = _mapper.Map<ProductDTO>(product);

            return Ok(result);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromForm] ProductDTO productDTO, [FromForm] IFormFile? image)
        {
            if (id != productDTO.Id)
                return BadRequest();


            Product product = _mapper.Map<Product>(productDTO);

            if (image is not null)
            {
                Product? dataProduct = await _unitOfWork.Products.Get(id);

                if (dataProduct.Image is not null)
                    await FileUpload.Delete(dataProduct.Image);

                product.Image = await FileUpload.Upload(image);
            }

            await _unitOfWork.Products.Put(id, product);

            await _unitOfWork.Commit();

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct([FromForm] IFormFile image, [FromForm] ProductDTO productDTO)
        {
            if (_unitOfWork.Products == null)
                return Problem("Entity set 'AppDbContext.Products'  is null.");


            Product product = _mapper.Map<Product>(productDTO);


            await _unitOfWork.Products.Post(product, await FileUpload.Upload(image));

            await _unitOfWork.Commit();

            return CreatedAtAction("GetProduct", new { id = product.Id }, _mapper.Map<ProductDTO>(product));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_unitOfWork.Products == null)
                return NotFound();

            var product = await _unitOfWork.Products.Delete(id);

            if (product == null)
                return NotFound();


            await FileUpload.Delete(product.Image!);

            await _unitOfWork.Commit();

            return NoContent();
        }

        //private bool ProductExists(int id)
        //{
        //    return (_unitOfWork.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
