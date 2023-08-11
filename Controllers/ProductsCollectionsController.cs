using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.EF.Data;
using Store.Core.Entities;
using Store.Core.UnitWork;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsCollectionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsCollectionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }


        // GET: api/ProductsCollections
        [HttpGet("GetCategoryProducts/{productCategoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(int productCategoryId)
        {
            if (_unitOfWork.Products == null)
                return NotFound();

            IEnumerable<Product> source = await _unitOfWork.Products.Get(Products => Products.CategoryId == productCategoryId);

            IEnumerable<ProductDTO> result = _mapper.Map<IEnumerable<ProductDTO>>(source);

            return Ok(result);
        }
    }
}
