using SOAPProduct.Models;
using System.ServiceModel;


[ServiceContract(Namespace = "https://localhost:7067/")]
public interface IProductService
{
    

    [OperationContract]
    List<Product> GetAll();

    [OperationContract]
    bool Add(Product product);

    [OperationContract]
    bool Update(Product product);

    [OperationContract]
    bool Delete(int id);
}
