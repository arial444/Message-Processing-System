using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ServiceA.Models;

namespace ServiceA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceA : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ServiceA(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            //Generate random number
            Random rnd = new Random();
            int rd = rnd.Next(1, 99999);
            RandomNumber randomNumber = new RandomNumber
            {
                Number = rd
            };

            //Save number in the DB
            try
            {
                Console.WriteLine("Saving random number into DB");
                await _appDbContext.RandomNumbers.AddAsync(randomNumber);
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                Console.WriteLine("Saving random number into DB has failed");
                Console.WriteLine(err);
                return NoContent();
            }

            return Ok(randomNumber);
        }
    }
}