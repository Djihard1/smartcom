using AutoMapper.Configuration;
using BL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Model.HelperClass;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
    class CustomerRepository : ICustomerRepository
 {
        private readonly Context _db;
        IConfiguration configuration;
        IUnitOfWork unitOfWork;

        public CustomerRepository(Context context)
        {
            
            _db = context;
          


        }

        public Task<ServiceResponse<List<Customer>>> AddCustomer(Customer customer, string password)
        {
            // Всё из Auth


            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Customer>>> DeleteCustomer(Guid id)
        {
            ServiceResponse<List<Customer>> serviceResponse = new ServiceResponse<List<Customer>>();

            Customer customer = await _db.Customers.FirstOrDefaultAsync(c => c.ID.Equals(id));

            try
            {
                if (customer != null)
                {
                    serviceResponse.Data = new List<Customer>();
                    _db.Customers.Remove(customer);
                    await _db.SaveChangesAsync();


                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Customer not found.";

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Customer>>> GetAllCustomer()
        {
            ServiceResponse<List<Customer>> serviceResponse = new ServiceResponse<List<Customer>>();
            serviceResponse.Data = await _db.Customers.ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<Customer>> GetCustomerById(Guid id)
        {
            ServiceResponse<Customer> serviceResponse = new ServiceResponse<Customer>();
            Customer dbCustomer = await _db.Customers.FirstOrDefaultAsync(c => c.ID.Equals(id));

            if (dbCustomer == null)
            {
                serviceResponse.Message = "ID not found.";
                serviceResponse.Success = false;
            }
            serviceResponse.Data = dbCustomer;
            return serviceResponse;
        }

       

        public async Task<ServiceResponse<Customer>> UpdateCustomer(Customer updCustomer)
        {
            ServiceResponse<Customer> serviceResponse = new ServiceResponse<Customer>();

            Customer customer = await _db.Customers.FirstOrDefaultAsync(c => c.ID.Equals(updCustomer.ID));
            if (customer != null)
            {
                customer.ADDRESS = updCustomer.ADDRESS;
                customer.CODE = updCustomer.CODE;
                customer.DISCOUNT = updCustomer.DISCOUNT;
                customer.Login = updCustomer.Login;
                customer.NAME = updCustomer.NAME;
                customer.Orders = updCustomer.Orders;
                customer.Role = updCustomer.Role;
                _db.Customers.Update(customer);
                await _db.SaveChangesAsync();
                return serviceResponse;


            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product not found.";

            }
            return serviceResponse;
        }
    }
}
