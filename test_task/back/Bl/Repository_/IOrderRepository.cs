using Model.HelperClass;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
   public interface IOrderRepository
    {

        Task<ServiceResponse<List<OrderR>>> AddOrder(OrderR orderR);
        Task<ServiceResponse<List<Order>>> GetMyOrders(Guid customerID);

        Task<ServiceResponse<List<Order>>> DeleteOrder(Guid Order);

        Task<ServiceResponse<Order>> UpdateOrder(Order order);

        Task<ServiceResponse<List<Order>>> GetAllOrder();


    }
}
