using CLassifiedsUIPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Provider
{

    public class EmployeeHelper
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://employeemicroservice3.azurewebsites.net");
            return client;
        }
    }
    public class EmployeeProvider : IEmployeeProvider
    {
        EmployeeHelper _api = new EmployeeHelper();
        PointsHelper _api2 = new PointsHelper();


        public async Task<HttpResponseMessage> ViewEmployeeOffers(int employeeId,string token)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/Employee/ViewEmployeeOffers/{employeeId}");

                return response;
            }
        }

        public async Task<HttpResponseMessage> ViewMostLikedOffers(int employeeId, string token)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/Employee/ViewMostLikedOffers/{employeeId}");

                return response;
            }
        }

        //public async Task<HttpResponseMessage> GetPointsByEmployeeId(int employeeId, string token)
        //{
        //    using (HttpClient client = _api2.Initial())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //        var response = await client.GetAsync($"api/Points/GetPointsByEmployeeId/{employeeId}");

        //        return response;
        //    }
        //}

        public async Task<HttpResponseMessage> GetEmployeeList(string token)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.GetAsync($"api/Employee/GetEmployeeList");

                return response;
            }
        }
    }
}
