using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AuthController : BaseAPIController
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterInputModel registerInputModel)
        {
            if (await UserWithUsernameExists(registerInputModel.Username))
            {
                return BadRequest(new {
                    Message = "Username is already taken!",
                    HasError = true
                });
            }

            if (await UserWithEmailExists(registerInputModel.Email))
            {
                return BadRequest(new {
                    Message = "Email is already taken!",
                    HasError = true
                });
            }

            using var hmac = new HMACSHA512();
            
            var user = new AppUser
            {
                UserName = registerInputModel.Username,
                Email = registerInputModel.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerInputModel.Password)),
                PasswordSalt = hmac.Key
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new {
                Message = "Successful registration!", HasSuccess = true
            });
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputModel loginInputModel)
        {
            return Ok("Successfully reached the login ednpoint");
        }

        [NonAction]
        private async Task<bool> UserWithUsernameExists(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserName == username);
        }

        private async Task<bool> UserWithEmailExists(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }
    }
}