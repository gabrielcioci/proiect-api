using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_II.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsService _carsService;

        public CarsController(CarsService service)
        {
            _carsService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetAll()
        {
            var cars = await _carsService.GetAllAsync();
            return Ok(cars);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Car>> GetById(string id)
        {
            var car = await _carsService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _carsService.CreateAsync(car);
            return Ok(car);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Car updatedCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedCar = await _carsService.GetByIdAsync(id);
            if (queriedCar == null)
            {
                return NotFound();
            }
            await _carsService.UpdateAsync(id, updatedCar);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var car = await _carsService.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            await _carsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
