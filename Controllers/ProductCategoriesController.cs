using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Elfie.Serialization;
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
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategories()
        {
            if (_unitOfWork.ProductCategories is null)
                return NotFound();


            IEnumerable<ProductCategoryDTO> result =
                _mapper.Map<IEnumerable<ProductCategoryDTO>>(
                        await _unitOfWork.ProductCategories.Get()
                    );

            return Ok(result);
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id?}")]
        public async Task<ActionResult<ProductCategoryDTO>> GetProductCategory(int id)
        {
            if (_unitOfWork.ProductCategories == null)
            {
                return NotFound();
            }
            ProductCategory? productCategory = await _unitOfWork.ProductCategories.Get(id);

            if (productCategory == null)
            {
                return NotFound();
            }
            ProductCategoryDTO result = _mapper.Map<ProductCategoryDTO>(productCategory);

            return Ok(result);
        }

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategoryDTO productCategoryDTO)
        {
            if (id != productCategoryDTO.Id)
            {
                return BadRequest();
            }

            ProductCategory productCategory = _mapper.Map<ProductCategory>(productCategoryDTO);

           await _unitOfWork.ProductCategories.Put(id, productCategory);

            await _unitOfWork.Commit();

            return NoContent();
        }

        // POST: api/ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDTO>> PostProductCategory(ProductCategoryDTO productCategoryDTO)
        {
            if (_unitOfWork.ProductCategories == null)
            {
                return Problem("Entity set 'AppDbContext.ProductCategories'  is null.");
            }

            ProductCategory productCategory = _mapper.Map<ProductCategory>(productCategoryDTO);
            await  _unitOfWork.ProductCategories.Post(productCategory);

            await _unitOfWork.Commit();

            return CreatedAtAction("GetProductCategory", new { id = productCategory.Id }, _mapper.Map<ProductCategoryDTO>(productCategory));
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            if (_unitOfWork.ProductCategories == null)
            {
                return NotFound();
            }

           await _unitOfWork.ProductCategories.Delete(id);

            await _unitOfWork.Commit();

            return NoContent();
        }

        //private bool ProductCategoryExists(int id)
        //{
        //    return (_unitOfWork.ProductCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
