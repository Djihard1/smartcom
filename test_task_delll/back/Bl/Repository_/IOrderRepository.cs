using Model.HelperClass;
using Model.HelperClass.MyOrder;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
   public interface IOrderRepository
    {

        Task<ServiceResponse<List<OrderPost>>> AddOrder(OrderPost[] orderR);
        Task<ServiceResponse<List<MyOrders>>> GetMyOrders(Guid customerID);

        Task<ServiceResponse<List<Order>>> DeleteOrder(Guid Order);

        Task<ServiceResponse<Order>> UpdateOrder(Order order);

        Task<ServiceResponse<List<OrderGetAll>>> GetAllOrder();


    }
}
