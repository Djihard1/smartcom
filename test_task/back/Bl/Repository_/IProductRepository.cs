using Model.HelperClass;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
  public  interface IProductRepository
    {
        Task<ServiceResponse<List<Product>>> GetAllProduct();
        Task<ServiceResponse<Product>> GetProductById(Guid id);
        Task<ServiceResponse<List<Product>>> AddProduct(Product product);
        Task<ServiceResponse<Product>> UpdateProduct(Product product);
        Task<ServiceResponse<List<Product>>> DeleteProduct(Guid id);
    }
}
