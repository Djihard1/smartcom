using Model.HelperClass;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
  public  interface IOrderItemRepository
    {
        Task<ServiceResponse<List<OrderItem>>> GetAllOrderItem();
        Task<ServiceResponse<OrderItem>> GetOrderItem(Guid id);
        Task<ServiceResponse<List<OrderItem>>> AddOrderItem(OrderItem orderItem);
        Task<ServiceResponse<OrderItem>> UpdateOrderItem(OrderItem orderItem);
        Task<ServiceResponse<List<OrderItem>>> DeleteOrderItem(Guid id);
    }
}
