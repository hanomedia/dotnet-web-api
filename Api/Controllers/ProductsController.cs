using Api.Dtos;
using Api.Errors;
using Api.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    public class ProductsController : BaseApiController
    {
        
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork,
        IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
            
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
           [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
            var products = await _unitOfWork.Repository<Product>().ListAsync(spec);
            var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);
            var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,totalItems,data));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);
            return _mapper.Map<Product,ProductToReturnDto>(product);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().ListAllAsync();
            return Ok(brands);

        }
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().ListAllAsync();
            return Ok(types);

        }
        [HttpPost("addproduct")]
        public async Task<ActionResult<Product>> CreateProduct(ProductToCreateDto productToCreateDto)
        {
            var product = _mapper.Map<ProductToCreateDto,Product>(productToCreateDto);
            product.PictureUrl = "images/products/placeholder.png";
            _unitOfWork.Repository<Product>().Add(product);
            var result = await _unitOfWork.Complete();
            if(result <= 0 ) return BadRequest(new ApiResponse(400, "Problem saving the product"));
            return Ok(product);
        }
        [HttpPut("editproduct/{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id,ProductToCreateDto productToUpdate)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            _mapper.Map(productToUpdate,product);
            _unitOfWork.Repository<Product>().Update(product);
            var result = await _unitOfWork.Complete();
            if(result <=0) return BadRequest(new ApiResponse(400,"Problem Updating the Product"));
            return Ok(product);
        }

        [HttpDelete("deleteproduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            _unitOfWork.Repository<Product>().Delete(product);
            var result =await _unitOfWork.Complete();
            if(result <= 0) return BadRequest(new ApiResponse(400,"Problem Deleting the product"));
            return Ok(product);
        }
    }
}