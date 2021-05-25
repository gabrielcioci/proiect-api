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
    public class ReparatiiController : ControllerBase
    {
        private readonly ReparatiiService reparatiiService;

        public ReparatiiController(ReparatiiService service)
        {
            reparatiiService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reparatie>>> GetAll()
        {
            var reparatii = await reparatiiService.GetAllAsync();
            return Ok(reparatii);
        }

        [AllowAnonymous]
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Reparatie>> GetById(string id)
        {
            var reparatie = await reparatiiService.GetByIdAsync(id);
            if (reparatie == null)
            {
                return NotFound();
            }
            return Ok(reparatie);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Reparatie reparatie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await reparatiiService.CreateAsync(reparatie);
            return Ok(reparatie);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Reparatie updatedReparatie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedReparatie = await reparatiiService.GetByIdAsync(id);
            if (queriedReparatie == null)
            {
                return NotFound();
            }
            await reparatiiService.UpdateAsync(id, updatedReparatie);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var reparatie = await reparatiiService.GetByIdAsync(id);
            if (reparatie == null)
            {
                return NotFound();
            }
            await reparatiiService.DeleteAsync(id);
            return NoContent();
        }
    }
}
