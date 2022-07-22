using EmployeeMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using OfferMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OfferMicroservice;



namespace EmployeeMicroservice.Controller
{


    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {

        OfferHelper _api = new OfferHelper();
        public static List<Employee> Employees;

        public EmployeeController()
        {
            Employees = new List<Employee>()
            {
                new Employee{EmployeeId=101, EmployeeName="Lakshmi Sruthi", Password="12345"},
                new Employee{EmployeeId=102, EmployeeName="Sindhu", Password="12345"},
                new Employee{EmployeeId=103, EmployeeName="Vijay", Password="12345"},
                new Employee{EmployeeId=104, EmployeeName="Harish", Password="12345"},
                new Employee{EmployeeId=105, EmployeeName="Chinmayee", Password="12345"},
                new Employee{EmployeeId=201, EmployeeName="Jata", Password="12345"},

            };
        }
        [HttpGet]
        [Route("GetEmployeeList")]
        public ActionResult GetEmployeeList()
        {
            return Ok(Employees);
        }

        //View All Offers
        [HttpGet()]
        [Route("ViewEmployeeOffers/{employeeId}")]
        public async Task<ActionResult> ViewEmployeeOffers(int employeeId)
        {

            List<OfferData> offers = new List<OfferData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Offer/GetOffersList");



            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                offers = JsonConvert.DeserializeObject<List<OfferData>>(results);
            }
            var employeeOffers = offers.Where(c => c.EmployeeId == employeeId).ToList();

            if (employeeOffers.Count == 0)
            {
                return NotFound("No offers found");
            }
            return Ok(employeeOffers);

        }



        [HttpGet]
        [Route("ViewMostLikedOffers/{employeeId}")]
        public async Task<ActionResult> ViewMostLikedOffers(int employeeId)
        {

            List<OfferData> offers = new List<OfferData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Offer/GetOffersList");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                offers = JsonConvert.DeserializeObject<List<OfferData>>(results);
            }
            var offer = (from c in offers where c.EmployeeId == employeeId orderby c.Likes descending select c).Take(3).ToList();
         
            if (offer.Count == 0)
            {
                return NotFound("No Offers Found");
            }
            return Ok(offer);

        }

        [HttpGet]
        [Route("GetPointsList")]
        public async Task<ActionResult> GetPointsList()
        {
            List<PointsData> offers = new List<PointsData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/points");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                offers = JsonConvert.DeserializeObject<List<PointsData>>(results);
            }

           
            return Ok(offers);
        }
    }
}
