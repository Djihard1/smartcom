using AutoMapper;
using BL.Repository_;
using BL.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context _db;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
       
        public UnitOfWork(Context _db, IConfiguration configuration)
        {
            this._db = _db;
            _configuration = configuration;
        }
        public IAuthRepository AuthRepository => new AuthRepository(_db, _configuration);

        public IProductRepository ProductRepository => new ProductRepository(_db,  _mapper);

        public ICustomerRepository CustomerRepository => new CustomerRepository(_db);
        public IOrderItemRepository OrderItemRepository => new OrderItemRepository(_db);

        public IOrderRepository OrderRepository => new OrderRepository(_db);

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
