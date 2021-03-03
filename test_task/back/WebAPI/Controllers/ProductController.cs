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
   
    public class ProductController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWorkN;

        public ProductController (IUnitOfWork _unitOfWorkN)
        {
            this._unitOfWorkN = _unitOfWorkN;
        }

        [HttpPost]
       // [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            return Ok(await _unitOfWorkN.ProductRepository.AddProduct(product)) ;
            
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWorkN.ProductRepository.GetAllProduct());
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(Guid id)
        {
            ServiceResponse<List<Product>> response = await _unitOfWorkN.ProductRepository.DeleteProduct(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            await _unitOfWorkN.SaveAsync();
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            return Ok(await _unitOfWorkN.ProductRepository.GetProductById(id));

        }

        [HttpPut]
       // [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            ServiceResponse<Product> response = await _unitOfWorkN.ProductRepository.UpdateProduct(product);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            await _unitOfWorkN.SaveAsync();
            return Ok(response);
        }


    }
}