using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService service)
        {
            _usersService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _usersService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _usersService.CreateAsync(user);
            return Ok(user);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedUser = await _usersService.GetByIdAsync(id);
            if (queriedUser == null)
            {
                return NotFound();
            }
            await _usersService.UpdateAsync(id, updatedUser);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _usersService.DeleteAsync(id);
            return NoContent();
        }
    }
}
