using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class PropertyController : BaseAPIController
    {
        private readonly ApplicationDbContext _dbContext;
        public PropertyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Property>>> GetAll()
        {
            return await this._dbContext.Properties.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await this._dbContext.Properties.FindAsync(id);

            if (property == null)
            {
                return NotFound(new {
                    ErrorMessage = "Property does not exist!",
                    HasError = true
                });
            }

            return Ok(new {
                Property = property
            });
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetAllByUser()
        {
            var username = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userByUsername = await this._dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
                
            if (userByUsername == null) {
                return Unauthorized(new {
                    Message = "Something went wrong while fetching you properties!",
                    HasError = true
                });
            }

            var properties = await this._dbContext.Properties.Where(x => x.UserId == userByUsername.Id).ToListAsync();

            return Ok(new {
                Properties = properties
            });
        }

        
        [HttpGet("new/data")]
        public async Task<IActionResult> GetNewPropertyInfo()
        {
            var locations = await this._dbContext.Locations.ToListAsync();

            return Ok(new {
                Locations = locations
            });
        }
        
    }
}