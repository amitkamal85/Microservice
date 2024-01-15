using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Models;
using VehicleAPI.Repository.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository=vehicleRepository;
        }
        // GET: api/<VehicleController>
        [HttpGet]
        public async Task<IEnumerable<Vehicle>> Get()
        {
            return await _vehicleRepository.GetVehicles();
        }

        // GET api/<VehicleController>/5
        [HttpGet("{id}")]
        public async Task<Vehicle> Get(int id)
        {
            return await _vehicleRepository.GetVehicleById(id);
        }

        // POST api/<VehicleController>
        [HttpPost]
        public async Task Post([FromBody] Vehicle vehicle)
        {
            await _vehicleRepository.AddVehicle(vehicle);
        }

        // PUT api/<VehicleController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Vehicle vehicle)
        {
            await _vehicleRepository.UpdateVehicle(id,vehicle);
        }

        // DELETE api/<VehicleController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _vehicleRepository.DeleteVehicle(id);
        }
    }
}
