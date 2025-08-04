using SoapProductApi.Data;
using System.Collections.Generic;
using System.Linq;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public List<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products.Find(id);
    }

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
        var product = _context.Products.Find(id);
        if (product == null) return false;
        _context.Products.Remove(product);
        return _context.SaveChanges() > 0;
    }
}
