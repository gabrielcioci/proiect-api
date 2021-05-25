using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_II.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService usersService;

        public UsersController(UsersService service)
        {
            usersService = service;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult> Login([FromBody] UserCredentials userCredentials)
        {
            var token = usersService.Authenticate(userCredentials.Email, userCredentials.Password);
            if (token == null)
                return Unauthorized();

            var user = await usersService.GetByEmailAsync(userCredentials.Email);
            return Ok(new { token, user.Id, user.post });
       
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await usersService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            var user = await usersService.GetByIdAsync(id);
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
            await usersService.CreateAsync(user);
            return Ok(user);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedUser = await usersService.GetByIdAsync(id);
            if (queriedUser == null)
            {
                return NotFound();
            }
            await usersService.UpdateAsync(id, updatedUser);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await usersService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await usersService.DeleteAsync(id);
            return NoContent();
        }
    }
}
