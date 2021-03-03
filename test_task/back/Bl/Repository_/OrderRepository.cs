using Microsoft.EntityFrameworkCore;
using Model.HelperClass;
using Model.HelperClass.MyOrder;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
    class OrderRepository : IOrderRepository
    {
        private readonly Context _db;
        public OrderRepository(Context context)
        {
            _db = context;
        }



        public async Task<ServiceResponse<List<OrderPost>>> AddOrder(OrderPost[] orderPosts)
        {

            ServiceResponse<List<OrderPost>> serviceResponse = new ServiceResponse<List<OrderPost>>();
            Random rnd = new Random();
            var ORDER_NUMBER = rnd.Next(100, 100000);
            var ORDER_DATE = DateTime.Now.Date;
            //TODO
            Customer customer = await _db.Customers.FirstOrDefaultAsync(c => c.ID == orderPosts[0].Product.customerId);
            Order order = new Order();
            order.STATUS = "Новый";
            order.ORDER_DATE = ORDER_DATE;
            order.SHIPMENT_DATE = DateTime.Parse(orderPosts[0].shipmentDate);
            order.ORDER_NUMBER = ORDER_NUMBER;
            order.Customer = customer;

            int total = 0;

            for (int i = 0; i < orderPosts.Length; i++)
            {
                Product product = await _db.Products.FirstOrDefaultAsync(c => c.ID == orderPosts[i].Product.ID);


                int countInt = Int32.Parse(orderPosts[i].quality);
                OrderItem orderItem = new OrderItem();
                orderItem.Order = order;
                orderItem.Product = product;
                orderItem.ITEMS_COUNT = countInt;
                orderItem.ITEM_PRICE = product.PRICE;
                await _db.OrderItems.AddAsync(orderItem);
                total += countInt * product.PRICE;


            }
            order.total = total;
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Order>>> DeleteOrder(Guid Order)
        {
            ServiceResponse<List<Order>> serviceResponse = new ServiceResponse<List<Order>>();

            Order order = await _db.Orders.FirstOrDefaultAsync(c => c.ID.Equals(Order));
            try
            {
                if (order != null)
                {
                    if (order.STATUS != "Выполнен")
                    {
                        _db.Orders.Remove(order);
                        await _db.SaveChangesAsync();
                        serviceResponse.Success = true;
                    }
                    else
                    {
                        serviceResponse.Message = "The order is completed. Removal is not possible.";
                        serviceResponse.Success = false;

                    }



                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Product not found.";

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<OrderGetAll>>> GetAllOrder()
        {
            ServiceResponse<List<OrderGetAll>> serviceResponse = new ServiceResponse<List<OrderGetAll>>();
            List<Order> orders = await _db.Orders.ToListAsync();
            List<OrderGetAll> orderGetAlls = new List<OrderGetAll>();
            foreach (var item in orders)
            {
                OrderGetAll orderGetAll = new OrderGetAll();
                orderGetAll.ID = item.ID;

                orderGetAll.ORDER_DATE = item.ORDER_DATE.ToShortDateString();
                orderGetAll.ORDER_NUMBER = item.ORDER_NUMBER;
                orderGetAll.OrrderItems = item.OrrderItems;
                orderGetAll.SHIPMENT_DATE = item.SHIPMENT_DATE.ToShortDateString();
                orderGetAll.STATUS = item.STATUS;
                orderGetAll.total = item.total;
                orderGetAlls.Add(orderGetAll);
            }
            serviceResponse.Data = orderGetAlls;

            return serviceResponse;
        }
        // TODO
        public async Task<ServiceResponse<List<MyOrders>>> GetMyOrders(Guid customerID)
        {
            ServiceResponse<List<MyOrders>> serviceResponse = new ServiceResponse<List<MyOrders>>();
            List<MyOrders> myOrders = new List<MyOrders>();

            List<Order> orders = await _db.Orders.Where(c => c.Customer.ID == customerID).ToListAsync();
            foreach (var item in orders)
            {

                MyOrders myorder = new MyOrders();
                myorder.ShipmentDate = item.SHIPMENT_DATE.ToShortDateString();
                myorder.OrderDate = item.ORDER_DATE.ToShortDateString();
                myorder.Status = item.STATUS;
                myorder.OrderNumber = item.ORDER_NUMBER;
                myorder.Total = item.total;
                myorder.Id = item.ID;
                List<Orders> orders1 = new List<Orders>();

                List<OrderItem> orderItems = _db.OrderItems.Include(c => c.Product).Where(c => c.Order.ID == item.ID).ToList();
                foreach (var items in orderItems)
                {
                    Orders orders_ = new Orders();
                    orders_.CATEGORY = items.Product.CATEGORY;
                    orders_.COUNT = items.ITEMS_COUNT;
                    orders_.NAME = items.Product.NAME;
                    orders_.PRICE = items.ITEM_PRICE;
                    orders1.Add(orders_);
                }
                myorder.order = orders1;
                myOrders.Add(myorder);
            }

            serviceResponse.Data = myOrders;
            if (serviceResponse == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Order not Found";
                return serviceResponse;

            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Order>> UpdateOrder(Order updOrder)
        {
            ServiceResponse<Order> serviceResponse = new ServiceResponse<Order>();

            Order order = await _db.Orders.FirstOrDefaultAsync(c => c.ID == updOrder.ID);
            if (order != null)
            {
                order.ID = updOrder.ID;
                order.ORDER_DATE = updOrder.ORDER_DATE;
                order.SHIPMENT_DATE = updOrder.SHIPMENT_DATE;
                order.STATUS = updOrder.STATUS;
                _db.Entry(order).State = EntityState.Modified;
                // await _db.SaveChangesAsync();
                serviceResponse.Data = order;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Order not found.";

            }
            return serviceResponse;

        }
    }
}

