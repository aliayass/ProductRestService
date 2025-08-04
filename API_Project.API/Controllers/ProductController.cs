using API_Project.API.Data;
using API_Project.API.Dto;
using API_Project.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API_Project.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var values = _context.Products.OrderBy(x => x.Id).ToList();

            return Ok(values);
        }
        [HttpGet("filter")]
        public IActionResult GetProducts(
    [FromQuery] ProductQueryFilterDTO filter) //HTTP GET isteğinde, URL sorgu parametrelerinden (query string) veri almak için kullanılır.

        {
            var query = _context.Products.AsQueryable(); //SQL’e çevrilebilir hâle getirir. 

            if (filter.id.HasValue)
                query = query.Where(p => p.Id == filter.id.Value);

            if (!string.IsNullOrEmpty(filter.barkod))
                query = query.Where(p => p.Barkod == filter.barkod);

            if (!string.IsNullOrEmpty(filter.renk))
                query = query.Where(p => p.Renk == filter.renk);

            if (!string.IsNullOrEmpty(filter.itemId))
                query = query.Where(p => p.ItemId == filter.itemId);

            if (!string.IsNullOrEmpty(filter.beden))
                query = query.Where(p => p.Beden == filter.beden);

            var products = query.ToList();

            if (products == null || products.Count == 0)
                return NotFound("Ürün bulunamadı.");

            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            try
            {
                if (product is null)
                    return BadRequest("Product cannot be null!!");

                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok("Yeni product ekleme işlemi başarılı!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct == null)
                return NotFound(new { message = $"ID {product.Id} olan ürün bulunamadı!" });

            existingProduct.Id = product.Id;
            existingProduct.Barkod = product.Barkod;
            existingProduct.ItemId = product.ItemId;
            existingProduct.Beden = product.Beden;
            existingProduct.Renk = product.Renk;
            existingProduct.Price = product.Price;
            existingProduct.StokSayisi = product.StokSayisi;

            _context.SaveChanges();
            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product is null)
                return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok($"Id {product.Id} olan ürün silindi!");
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneProduct([FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Product> productPatch)
        {
            // check: product mevcut mu
            var entity = _context.Products.Find(id);

            if (entity is null)
                return NotFound(new
                {
                    StatusCode = 404,
                    message = $"Book with id: {id} not found."
                });
            productPatch.ApplyTo(entity);
            return NoContent();
        }

    }
}
