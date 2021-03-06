using Model.HelperClass;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
   public interface IAuthRepository
    {
        Task<ServiceResponse<Guid>> Register(Customer customer, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);

        Task<ServiceResponse<Customer>> GetIdByLogin(string login);

    }
}
