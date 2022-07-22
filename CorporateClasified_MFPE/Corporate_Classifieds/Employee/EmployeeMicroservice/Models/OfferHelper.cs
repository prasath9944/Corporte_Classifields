using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OfferMicroservice.Models
{
    public class OfferHelper
    {
        //HttpClient class provides class for sending HTTP request and receiving HTTP response from a resource identified by URL
        public HttpClient Initial()
        {
            var client = new HttpClient(); //created an instance of HTTP class
            client.BaseAddress = new Uri("https://localhost:44389/");
            return client;
        }
    }
}
