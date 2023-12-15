using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts()
        {
            var products = await _context.Products.
            Include(p => p.ProductBrand).
            Include(p => p.ProductType).
            ToListAsync();
            return Ok(products);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.
            Include(p => p.ProductBrand).Include(p => p.ProductType).FirstOrDefaultAsync(p => p.Id == id);
            return Ok(product);

        }
        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetBrands()
        {
            var brands = await _context.ProductBrands.ToListAsync();
            return Ok(brands);

        }
        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetTypes()
        {
            var types = await _context.ProductTypes.ToListAsync();
            return Ok(types);

        }
    }
}