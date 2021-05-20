using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientiController : ControllerBase
    {
        private readonly ClientiService _clientiService;

        public ClientiController(ClientiService service)
        {
            _clientiService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAll()
        {
            var clienti = await _clientiService.GetAllAsync();
            return Ok(clienti);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Client>> GetById(string id)
        {
            var client = await _clientiService.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _clientiService.CreateAsync(client);
            return Ok(client);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Client updatedClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedClient = await _clientiService.GetByIdAsync(id);
            if (queriedClient == null)
            {
                return NotFound();
            }
            await _clientiService.UpdateAsync(id, updatedClient);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await _clientiService.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            await _clientiService.DeleteAsync(id);
            return NoContent();
        }
    }
}
