using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceB.Models;

namespace ServiceB.Clients
{
    public class ServiceBClient
    {
        private readonly HttpClient httpClient;

        public ServiceBClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<RandomNumber> GetRandomNumberAsync()
        {
            //Getting info that there is a number to use
            try
            {
                Console.WriteLine("Getting data from ServiceB");
                await httpClient.GetAsync("/api/ServiceB");
                RandomNumber number = await httpClient.GetFromJsonAsync<RandomNumber>("/api/ServiceB");
                return number;
            }
            catch (Exception err)
            {
                Console.WriteLine("Getting data from ServiceB has failed");
                Console.WriteLine(err);
                return null;
            }
        }
    }
}