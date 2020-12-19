using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Models.InputModels;
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
            var propertyTypes = await this._dbContext.PropertyTypes.ToListAsync();

            return Ok(new {
                Locations = locations,
                PropertyTypes = propertyTypes
            });
        } 

        [Authorize]
        [HttpPost("new")]
        public async Task<IActionResult> New(NewPropertyInputModel propertyInputModel)
        {
            var locationId = propertyInputModel.Location;
            var location = await this._dbContext.Locations.FirstOrDefaultAsync(l => l.Id == locationId);

            if (location == null) {
                return BadRequest(new {
                    Message = "Invalid location!",
                    HasFormError = true
                });
            }

            var typeId = propertyInputModel.Type;
            var propertyType = await this._dbContext.PropertyTypes.FirstOrDefaultAsync(t => t.Id == typeId);

            if (propertyType == null) {
                return BadRequest(new {
                    Mesasge = "Invalid property type!",
                    HasFormError = true
                });
            }

            var username = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userByUsername = await this._dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
                
            if (userByUsername == null) {
                return Unauthorized(new {
                    Message = "Something went wrong while fetching you properties!",
                    HasError = true
                });
            }

            var property = new Property
            {
                Location = location,
                PropertyType = propertyType,
                User = userByUsername,
                Price = propertyInputModel.Price,
                Bedrooms = propertyInputModel.Bedrooms,
                Description = propertyInputModel.Description
            };

            try {
                await this._dbContext.Properties.AddAsync(property);
                await _dbContext.SaveChangesAsync();
            } catch {
                return BadRequest(new {
                    Message = "An error occurred while adding new property!",
                    HasError = true
                });
            }
            return Ok(new {
                SuccessMessage = "Successfully added a new property!",
                HasSuccess = true
            });
        }       
    }
}