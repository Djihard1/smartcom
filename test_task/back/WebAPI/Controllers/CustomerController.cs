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
   // [Authorize(Roles = "Manager")]
    public class CustomerController : ControllerBase


    {
        private readonly IUnitOfWork _unitOfWorkN;

        public CustomerController(IUnitOfWork _unitOfWorkN)
        {
            this._unitOfWorkN = _unitOfWorkN;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegister request)
        {
            ServiceResponse<Guid> response = await _unitOfWorkN.AuthRepository.Register(
                new Customer { Login = request.Username, Role = request.Role, NAME = request.NAME, CODE = request.CODE, DISCOUNT = request.DISCOUNT, ADDRESS = request.ADDRESS }, request.Password
            );
            await _unitOfWorkN.SaveAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

      


        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWorkN.CustomerRepository.GetAllCustomer());
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            ServiceResponse<Customer> response = await _unitOfWorkN.CustomerRepository.UpdateCustomer(customer);
            if (response.Success)
            {
                
                return Ok(response);
            }
            return NotFound(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            ServiceResponse<List<Customer>> response = await _unitOfWorkN.CustomerRepository.DeleteCustomer(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }



    }
}
