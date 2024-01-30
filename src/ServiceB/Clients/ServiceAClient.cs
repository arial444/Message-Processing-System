using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceB.Models;

namespace ServiceA.Clients
{
    public class ServiceAClient
    {
        private readonly HttpClient httpClient;
        private readonly AppDbContext _appDbContext;

        public ServiceAClient(HttpClient httpClient, AppDbContext appDbContext)
        {
            this.httpClient = httpClient;
            _appDbContext = appDbContext;
        }

        public async Task<RandomNumber> GetRandomNumberAsync()
        {
            //Getting info that there is a number to use
            try
            {
                Console.WriteLine("Getting data from ServiceA");
                var number = await httpClient.GetFromJsonAsync<RandomNumber>("/api/ServiceA");
                return number;
            }
            catch (Exception err)
            {
                Console.WriteLine("Getting data from ServiceA has failed");
                Console.WriteLine(err);
                return null;
            }
        }
    }
}