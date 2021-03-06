using BL.Repository_;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.UnitOfWork
{
  public  interface IUnitOfWork
    {
        IAuthRepository AuthRepository { get; }
        IProductRepository ProductRepository { get; }

        ICustomerRepository CustomerRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }

        IOrderRepository OrderRepository { get; }

        Task<bool> SaveAsync();
    }
}
