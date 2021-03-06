using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.UnitOfWork;
using Model.Model;
using Model.HelperClass;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase


    {
        private readonly IUnitOfWork _unitOfWorkN;
        public OrderItemController(IUnitOfWork _unitOfWorkN)
        {
            this._unitOfWorkN = _unitOfWorkN;
        }
        [HttpPost]
        public async Task<IActionResult> AddOrderItem(OrderItem order)
        {
            return Ok(await _unitOfWorkN.OrderItemRepository.AddOrderItem(order));

        }



        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWorkN.OrderItemRepository.GetAllOrderItem());
        }

      

    }
}