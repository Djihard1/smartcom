using AutoMapper;
using Microsoft.AspNetCore.Http;
using Model.HelperClass;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BL.Repository_
{
    class ProductRepository : IProductRepository
    {
       private readonly IMapper _mapper;
        private readonly Context _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductRepository( Context context, IMapper mapper)
        {
            //  _httpContextAccessor = httpContextAccessor;
            _db = context;
            _mapper = mapper;

        }


        private Guid GetUserId() => Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);



        public async Task<ServiceResponse<List<Product>>> AddProduct(Product newproduct)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();
            //Product product = await _context.Products.FirstOrDefaultAsync(x => x.NAME.ToLower().Equals(newproduct.NAME.ToLower()));

            if (await ProductExists(newproduct.NAME))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product already exists.";
                
                return serviceResponse;
            }
            await _db.Products.AddAsync(newproduct);
            await _db.SaveChangesAsync();
            serviceResponse.Message = "Product added";
            serviceResponse.Success = true;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Product>>> DeleteProduct(Guid id)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();

            Product product = await _db.Products.FirstOrDefaultAsync(c => c.ID.Equals(id));

            try
            {
                if (product != null)
                {
                    serviceResponse.Data = new List<Product>();
                    _db.Products.Remove(product);
                    await _db.SaveChangesAsync();


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

        public async Task<ServiceResponse<List<Product>>> GetAllProduct()
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();
            serviceResponse.Data = await _db.Products.ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<Product>> GetProductById(Guid id)
        {
            ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();
            Product dbProduct = await _db.Products.FirstOrDefaultAsync(c => c.ID.Equals(id));
           
            if (dbProduct == null)
            {
                serviceResponse.Message = "ID not found.";
                serviceResponse.Success = false;
            }
            serviceResponse.Data = dbProduct;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Product>> UpdateProduct(Product updProduct)
        {
            ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();

            Product product = await _db.Products.FirstOrDefaultAsync(c => c.ID == updProduct.ID);
            if (product != null) { 
            product.ID = updProduct.ID;
            product.CODE = updProduct.CODE;
            product.NAME = updProduct.NAME;
            product.PRICE = updProduct.PRICE;
            product.CATEGORY = updProduct.CATEGORY;
               // product.img = updProduct.img;
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
                serviceResponse.Data = product;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product not found.";

            }
            serviceResponse.Success = true;
            return serviceResponse;

        }

        public async Task<bool> ProductExists(string productName)
        {
            if (await _db.Products.AnyAsync(x => x.NAME.ToLower() == productName.ToLower()))
            {
                return true;
            }
            return false;
        }

       
    }
}
