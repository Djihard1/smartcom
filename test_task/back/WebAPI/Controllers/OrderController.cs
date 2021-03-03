using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.HelperClass;
using Model.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
       

            private readonly IUnitOfWork _unitOfWorkN;

            public OrderController (IUnitOfWork _unitOfWorkN)
            {
                this._unitOfWorkN = _unitOfWorkN;
            }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderPost[] orderPosts)
        {
            return Ok(await _unitOfWorkN.OrderRepository.AddOrder(orderPosts));

        }
        [HttpGet("{customerId}")]
       
        public async Task<IActionResult> Get(Guid customerId)
        {
            return Ok(await _unitOfWorkN.OrderRepository.GetMyOrders(customerId));
        }

        [HttpDelete("{id}")]
       

        public async Task<IActionResult> Delete(Guid id)
        {
            ServiceResponse<List<Order>> response = await _unitOfWorkN.OrderRepository.DeleteOrder(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            await _unitOfWorkN.SaveAsync();
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            ServiceResponse<Order> response = await _unitOfWorkN.OrderRepository.UpdateOrder(order);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            await _unitOfWorkN.SaveAsync();
            return Ok(response);
        }
        [HttpGet("GetAll")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWorkN.OrderRepository.GetAllOrder());
        }



    }
}