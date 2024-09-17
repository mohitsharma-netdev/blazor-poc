using FrontEndApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FrontEndApiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        string externalApi1Url = "https://localhost:5001/";
        string externalApi2Url = "https://localhost:6001/";

        public FrontEndApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Invokes ExternalAPI1 and ExternalAPI2 asynchronously using GET method.
        /// </summary>
        /// <returns>Combined response from ExternalAPI1 and ExternalAPI2</returns>
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts(string SearchText)
        {
            var api1Task = _httpClient.GetStringAsync(externalApi1Url + $"api/ExternalAPI1?SearchText={SearchText}");
            var api2Task = _httpClient.GetStringAsync(externalApi2Url + $"api/ExternalAPI2?SearchText={SearchText}");

            await Task.WhenAll(api1Task, api2Task);

            var result = new InvokeApisResponse
            {
                ExternalAPI_1_Response = await api1Task,
                ExternalAPI_2_Response = await api2Task
            };

            return Ok(result);
        }

        /// <summary>
        /// Invokes ExternalAPI1 and ExternalAPI2  asynchronously using POST method.
        /// </summary>
        /// <param name="data">Data to be posted to ExternalAPI1 and ExternalAPI2 </param>
        /// <returns>Combined response from ExternalAPI1 and ExternalAPI2 </returns>
        [HttpPost("GetServices")]
        public async Task<IActionResult> GetServices([FromBody] object data)
        {
            var api1Task = _httpClient.PostAsJsonAsync(externalApi1Url + "api/ExternalAPI1", data);
            var api2Task = _httpClient.PostAsJsonAsync(externalApi2Url + "api/ExternalAPI2", data);

            await Task.WhenAll(api1Task, api2Task);

            var result = new InvokeApisResponse
            {
                ExternalAPI_1_Response = await api1Task.Result.Content.ReadAsStringAsync(),
                ExternalAPI_2_Response = await api2Task.Result.Content.ReadAsStringAsync()
            };

            return Ok(result);
        }
    }
}
