using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceA.Clients;
using ServiceB.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace ServiceB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceB : ControllerBase
    {

        private readonly ServiceAClient serviceAClient;
        private readonly AppDbContext _appDbContext;
        private readonly IDistributedCache _distributedCache;

        public ServiceB(ServiceAClient serviceAClient, AppDbContext appDbContext, IDistributedCache distributedCache)
        {
            this.serviceAClient = serviceAClient;
            _appDbContext = appDbContext;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> Multiply()
        {
            var number = await serviceAClient.GetRandomNumberAsync();

            if (number != null)
            {
                try
                {
                    //Check if the Client get any data
                    Console.WriteLine("Getting random number from DB");
                    var num = await _appDbContext.RandomNumbers.OrderBy(e => e.ID).LastOrDefaultAsync();

                    if (num != null)
                    {
                        //Multiply the random number
                        var multipliedNumber = num?.Number * 2;

                        //Storing multiplied number in cahche
                        Console.WriteLine("Storing multiplied number in cache");
                        await _distributedCache.SetStringAsync("number", multipliedNumber.ToString());

                        RandomNumber multipliedRandomNumber = new RandomNumber
                        {
                            ID = number.ID,
                            Number = (int)multipliedNumber
                        };

                        return Ok(multipliedRandomNumber);
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