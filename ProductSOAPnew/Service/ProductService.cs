using ProductSOAPnew.Data;
using ProductSOAPnew.Models;

namespace ProductSOAPnew.SOAP
{
    public class ProductSoapService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductSoapService(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll() => _context.Products.ToList();

        public Product? GetById(int id) => _context.Products.Find(id);

        public bool Add(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChanges() > 0;
        }

        public bool Update(Product product)
        {
            _context.Products.Update(product);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var entity = _context.Products.Find(id);
            if (entity == null) return false;

            _context.Products.Remove(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
