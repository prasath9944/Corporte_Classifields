using CLassifiedsUIPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Provider
{
    public class PointsHelper
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://pointsmicroservice2.azurewebsites.net");
            return client;
        }
    }
    public class PointsProvider : IPointsProvider
    {
        PointsHelper _api = new PointsHelper();
        public async Task<HttpResponseMessage> GetPointsByEmployeeId(int employeeId, string token)
        {
           
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/Points/GetPointsByEmployeeId/{employeeId}");
                

                return response;
            }
        }

       

        public async Task<HttpResponseMessage> RefreshPointsByEmployee(int employeeId, string token)
        {
           
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/Points/RefreshPointsByEmployee/{employeeId}");


                return response;
            }
        }

      
    }
}
