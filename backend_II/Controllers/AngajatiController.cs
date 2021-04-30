using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AngajatiController : ControllerBase
    {
        private readonly AngajatiService _angajatiService;

        public AngajatiController(AngajatiService service)
        {
            _angajatiService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Angajat>>> GetAll()
        {
            var angajati = await _angajatiService.GetAllAsync();
            return Ok(angajati);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Angajat>> GetById(string id)
        {
            var angajat = await _angajatiService.GetByIdAsync(id);
            if (angajat == null)
            {
                return NotFound();
            }
            return Ok(angajat);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Angajat angajat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _angajatiService.CreateAsync(angajat);
            return Ok(angajat);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Angajat updatedAngajat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedAngajat = await _angajatiService.GetByIdAsync(id);
            if (queriedAngajat == null)
            {
                return NotFound();
            }
            await _angajatiService.UpdateAsync(id, updatedAngajat);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var angajat = await _angajatiService.GetByIdAsync(id);
            if (angajat == null)
            {
                return NotFound();
            }
            await _angajatiService.DeleteAsync(id);
            return NoContent();
        }
    }
}
