using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparatiiController : ControllerBase
    {
        private readonly ReparatiiService _reparatiiService;

        public ReparatiiController(ReparatiiService service)
        {
            _reparatiiService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reparatie>>> GetAll()
        {
            var reparatii = await _reparatiiService.GetAllAsync();
            return Ok(reparatii);
        }

        public async Task<ActionResult<Reparatie>> GetById(string id)
        {
            var reparatii = await _reparatiiService.GetByIdAsync(id);
            if (reparatii == null)
            {
                return NotFound();
            }
            return Ok(reparatii);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reparatie reparatie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _reparatiiService.CreateAsync(reparatie);
            return Ok(reparatie);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Reparatie updatedReparatie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedReparatie = await _reparatiiService.GetByIdAsync(id);
            if (queriedReparatie == null)
            {
                return NotFound();
            }
            await _reparatiiService.UpdateAsync(id, updatedReparatie);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var reparatie = await _reparatiiService.GetByIdAsync(id);
            if (reparatie == null)
            {
                return NotFound();
            }
            await _reparatiiService.DeleteAsync(id);
            return NoContent();
        }
    }
}
