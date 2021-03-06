using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.HelperClass;
using Model.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository_
{
    public class AuthRepository : IAuthRepository
    {
        private Context _db;
        private readonly IConfiguration _configuration;

        public AuthRepository(Context context, IConfiguration configuration)
        {
            this._db = context;
            _configuration = configuration;
        }



        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            Customer customer = await _db.Customers.FirstOrDefaultAsync(x => x.Login.ToLower().Equals(username.ToLower())); ;
            if (customer == null)
            {
                response.Success = false;
                response.Message = "User Name or password error";
            }
            else if (!VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
            {
                response.Success = false;
                response.Message = "User Name or password error";
            }
            else
            {
                response.Data = CreateToken(customer);
            }
            //response.Message = customer.ID.ToString();

            return response;

        }

        public async Task<ServiceResponse<Guid>> Register(Customer customer, string password)
        {

            ServiceResponse<Guid> response = new ServiceResponse<Guid>();

            if (await UserExists(customer.Login))
            {
                response.Success = false;
                response.Message = "User already exists.";
                return response;
            }


            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            await _db.Customers.AddAsync(customer);
            var name = customer.Login;
            response.Data = customer.ID;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _db.Customers.AnyAsync(x => x.Login.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        // Создаем кэш пароля 

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // Проверка кэша
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        // Выдача токена 

        private string CreateToken(Customer customer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.ID.ToString()),
                new Claim(ClaimTypes.Name, customer.Login),
                new Claim(ClaimTypes.Role, customer.Role)

            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public async Task<ServiceResponse<Customer>> GetIdByLogin(string login)
        {
            ServiceResponse<Customer> serviceResponse = new ServiceResponse<Customer>();
            Customer customer = await _db.Customers.FirstOrDefaultAsync(c => c.Login.ToLower().Equals(login.ToLower()));

            if (customer == null)
            {
                serviceResponse.Message = "Login not found.";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            //TODO
            else { 
            byte[] tmp = { };
            customer.ADDRESS = "";
            customer.CODE = "";
            customer.DISCOUNT = 0;
            customer.Login = "";
            customer.NAME = "";
            customer.PasswordHash = tmp;
            customer.PasswordSalt = tmp;


            serviceResponse.Data = customer;



            return serviceResponse;
            }
        }
    }
}
