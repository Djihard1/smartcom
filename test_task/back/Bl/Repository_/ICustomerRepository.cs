using Model.HelperClass;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
   public interface ICustomerRepository
    {
        Task<ServiceResponse<List<Customer>>> GetAllCustomer();
        Task<ServiceResponse<Customer>> GetCustomerById(Guid id);

        Task<ServiceResponse<List<Customer>>> AddCustomer(Customer customer, string password);
        Task<ServiceResponse<Customer>> UpdateCustomer(Customer customer);
        Task<ServiceResponse<List<Customer>>> DeleteCustomer(Guid id);

    }
}
