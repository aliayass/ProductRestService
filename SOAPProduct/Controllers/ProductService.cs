using AutoMapper;
using SOAPProduct.Data;
using SOAPProduct.Dto;
using SOAPProduct.Models;
namespace SOAPProduct.Controllers
{
    public class ProductService : IProductService
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public ProductService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }



        public bool DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            return true;
        }

        public List<Product> GetAll()
        {
        
            return _context.Products.ToList(); // ✅ Direkt Product listesi döndür
        
        }

        

        private List<Product> Ok(List<ProductDto> productDtos)
        {
            throw new NotImplementedException();
        }

        public Product? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Add(Product product)
        {
      //      _context.Products.Add(product); // ✅ Direkt gelen product'ı ekle
            return _context.SaveChanges() > 0;
        }

        public bool Update(Product product)
        {
            var value = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (value == null)
                return false;
            value.Barkod = product.Barkod;
            value.Renk = product.Renk;
            value.ItemId = product.ItemId;
            value.Beden = product.Beden;
            value.Price = product.Price;

            _context.Products.Update(value);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var value = _context.Products.Find(id);
            _context.Remove(value);
            return _context.SaveChanges() > 0;


        }
    }
}
