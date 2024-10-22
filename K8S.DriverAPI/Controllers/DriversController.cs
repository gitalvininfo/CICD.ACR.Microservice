using K8S.DriverAPI.Data.Repositories.Interfaces;
using K8S.DriverAPI.DTOs.Requests;
using K8S.DriverAPI.DTOs.Responses;
using K8S.DriverAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace K8S.DriverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public DriversController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> GetDriver(Guid driverId)
        {
            var driver = await _unitOfWork.Drivers.GetById(driverId);

            if (driver == null)
                return NotFound();

            var result = driver.Adapt<GetDriverResponse>();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _unitOfWork.Drivers.All();

            var result = drivers.Adapt<IEnumerable<GetDriverResponse>>();

            await TestConnection();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest driver)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var result = driver.Adapt<Driver>();

            await _unitOfWork.Drivers.Add(result);

            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDriver), new { driverId = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequest driver)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = driver.Adapt<Driver>();

            await _unitOfWork.Drivers.Update(result);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{driverId:Guid}")]
        public async Task<IActionResult> DeleteDriver(Guid driverId)
        {
            var driver = await _unitOfWork.Drivers.GetById(driverId);

            if (driver == null)
                return NotFound();

            await _unitOfWork.Drivers.Delete(driverId);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        public async Task<IActionResult> TestConnection()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{_configuration.GetConnectionString("DriverStatAPI")}/DriverStats");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"HTTP Communication Here: DriverStatAPI -> 🔥🔥🔥");


                    Console.WriteLine($"DriverStatAPI URL: {_configuration.GetConnectionString("DriverStatAPI")}");

                    string message = await response.Content.ReadAsStringAsync();

                    return Ok(message);
                }

                Console.WriteLine("Http test connection failed on DriverStatAPI");

                return BadRequest("Http test connection failed on DriverStatAPI");
            }
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Test connection works!!!");
        }

    } 
}
