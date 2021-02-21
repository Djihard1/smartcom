using Microsoft.EntityFrameworkCore;
using Model.HelperClass;
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
        public OrderRepository (Context context)
        {

            _db = context;



        }

        public async Task<ServiceResponse<List<OrderR>>> AddOrder(OrderR orderR)
        {
            ServiceResponse<List<OrderR>> serviceResponse = new ServiceResponse<List<OrderR>>();
            Random rnd = new Random();
            

            Customer customer = await _db.Customers.FirstOrDefaultAsync(c => c.ID == orderR.CustomerId);
            // customer.ID = customerId;

            Order order = new Order();
            order.Customer = customer;
            order.STATUS = "Новый";
            order.ORDER_DATE = DateTime.Now;
            order.SHIPMENT_DATE = orderR.Shipment_date;
            order.ORDER_NUMBER = rnd.Next(100, 100000);
           

            Product product = await _db.Products.FirstOrDefaultAsync(c => c.ID == orderR.ProductId);

            OrderItem orderItem = new OrderItem();
            orderItem.Order = order;
            orderItem.Product = product;
            orderItem.ITEMS_COUNT = orderR.Item_count;
            orderItem.ITEM_PRICE = product.PRICE;

            await _db.Orders.AddAsync(order);
            await _db.OrderItems.AddAsync(orderItem);
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
                    if(order.STATUS != "Выполнен") {
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

        public async Task<ServiceResponse<List<Order>>> GetAllOrder()
        {
            ServiceResponse<List<Order>> serviceResponse = new ServiceResponse<List<Order>>();
            serviceResponse.Data = await _db.Orders.ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Order>>> GetMyOrders(Guid customerID)
        {
            ServiceResponse<List<Order>> serviceResponse = new ServiceResponse<List<Order>>();
            serviceResponse.Data = await _db.Orders.Where(c=> c.Customer.ID == customerID).ToListAsync();
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

