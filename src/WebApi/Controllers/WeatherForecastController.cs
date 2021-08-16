using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private string ApiUrl { get; set; }
        
        private IHttpClientFactory _clientFactory { get; }
        

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory http, IOptions<WeatherApiOptions> options)
        {
            _logger        = logger;
            ApiUrl         = options.Value.Url;
            _clientFactory = http;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var client   = _clientFactory.CreateClient();
                var response = await client.GetAsync($"{ApiUrl}/weatherforecast");
                return Ok(await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>());
            }
            catch
            {
                return Problem("Soory, service is not available");
            }
        }
    }
}