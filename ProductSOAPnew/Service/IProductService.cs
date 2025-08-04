using ProductSOAPnew.Models;
using System.ServiceModel;
using System.Collections.Generic;

namespace ProductSOAPnew.SOAP
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        List<Product> GetAll();

        [OperationContract]
        Product? GetById(int id);

        [OperationContract]
        bool Add(Product product);

        [OperationContract]
        bool Update(Product product);

        [OperationContract]
        bool Delete(int id);
    }
}
