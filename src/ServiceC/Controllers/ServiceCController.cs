using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ServiceB.Clients;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;

namespace ServiceC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceC : ControllerBase
    {

        private readonly ServiceBClient serviceBClient;
        private readonly IDistributedCache _distributedCache;

        public ServiceC(ServiceBClient serviceBClient, IDistributedCache distributedCache)
        {
            this.serviceBClient = serviceBClient;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            //Check if the Client get any data
            var number = await serviceBClient.GetRandomNumberAsync();

            if (number != null)
            {
                //Pulling the multiplied number from the cache
                try
                {
                    var cachedNumber = await _distributedCache.GetStringAsync("number");

                    if (cachedNumber != null)
                    {
                        return Ok(cachedNumber);
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine("Error has Occured");
                    Console.WriteLine(err);
                }
            }

            return NoContent();
        }
    }
}