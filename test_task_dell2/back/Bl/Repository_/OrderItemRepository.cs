using Microsoft.EntityFrameworkCore;
using Model.HelperClass;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
    class OrderItemRepository : IOrderItemRepository
    {
        private readonly Context _db;
        public OrderItemRepository(Context context)
        {

            _db = context;


        }

        public async Task<ServiceResponse<List<OrderItem>>> AddOrderItem(OrderItem orderItem)
        {
            ServiceResponse<List<OrderItem>> serviceResponse = new ServiceResponse<List<OrderItem>>();
            await _db.OrderItems.AddAsync(orderItem);
            await _db.SaveChangesAsync();
            serviceResponse.Data = new List<OrderItem>();
            return serviceResponse;

        }

        public Task<ServiceResponse<List<OrderItem>>> DeleteOrderItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<OrderItem>>> GetAllOrderItem()
        {
            ServiceResponse<List<OrderItem>> serviceResponse = new ServiceResponse<List<OrderItem>>();
            serviceResponse.Data = await _db.OrderItems.ToListAsync();
            return serviceResponse;

        }

        public async Task<ServiceResponse<OrderItem>> GetOrderItem(Guid id)
        {
            ServiceResponse<OrderItem> serviceResponse = new ServiceResponse<OrderItem>();
            OrderItem dbOrderItem = await _db.OrderItems.FirstOrDefaultAsync(c => c.ID.Equals(id));

            if (dbOrderItem == null)
            {
                serviceResponse.Message = "ID not found.";
                serviceResponse.Success = false;
            }
            serviceResponse.Data = dbOrderItem;
            return serviceResponse;
        }

        public async Task<ServiceResponse<OrderItem>> UpdateOrderItem(OrderItem updOrderItem)
        {
            ServiceResponse<OrderItem> serviceResponse = new ServiceResponse<OrderItem>();

            OrderItem orderItem = await _db.OrderItems.FirstOrDefaultAsync(c => c.ID == updOrderItem.ID);
            if (orderItem != null)
            {
                orderItem.ID = updOrderItem.ID;
                orderItem.Order = updOrderItem.Order;
                orderItem.Product = updOrderItem.Product;
                orderItem.ITEMS_COUNT = updOrderItem.ITEMS_COUNT;
                orderItem.ITEM_PRICE = updOrderItem.ITEM_PRICE;
                _db.Entry(orderItem).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                serviceResponse.Data = orderItem;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "OrderItem not found.";

            }
            return serviceResponse;

        }
    }
    }

