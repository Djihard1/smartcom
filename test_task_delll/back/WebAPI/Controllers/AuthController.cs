using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Repository_;
using BL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.HelperClass;
using Model.Model;


namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWorkN;

        public AuthController(IUnitOfWork _unitOfWorkN)
        {
            this._unitOfWorkN = _unitOfWorkN;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegister request)
        {
            ServiceResponse<Guid> response = await _unitOfWorkN.AuthRepository.Register(
                new Customer { Login = request.Username, Role = request.Role, NAME = request.NAME, CODE = request.CODE, DISCOUNT =request.DISCOUNT, ADDRESS = request.ADDRESS }, request.Password
            );
            await _unitOfWorkN.SaveAsync();
            if (!response.Success)
            {
                return Ok(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin request)
        {
            ServiceResponse<string> response = await _unitOfWorkN.AuthRepository.Login(
                request.Username, request.Password);
            if (!response.Success)
            {
                return Ok(response);
            }
            return Ok(response);
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetSingle(string login)
        {
            return Ok(await _unitOfWorkN.AuthRepository.GetIdByLogin(login));

        }


    }
}