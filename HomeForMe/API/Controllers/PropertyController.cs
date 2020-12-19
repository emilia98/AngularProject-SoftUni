using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
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
        public async Task<ActionResult<Property>> GetById(int id)
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
                property = property
            });
        }
    }
}