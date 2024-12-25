using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Aggregation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Aggregation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherCityController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherCityController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeatherAsync(string city)
        {


            try
            {
                var response = await _weatherService.GetWeatherAsync(city);


                if (response == null)
                {
                    return NotFound(new { message = $"No weather data cound for this city " });
                }
                return Ok(response);


            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}

