using CLassifiedsUIPortal.Models;
using CLassifiedsUIPortal.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Controllers
{
    public class EmployeeController : Controller
    {

        EmployeeHelper _api = new EmployeeHelper();

        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(EmployeeController));
        private readonly IEmployeeProvider _provider;
        private readonly IPointsProvider points_provider;

        public EmployeeController(IEmployeeProvider provider, IPointsProvider _provider)
        {
            this._provider = provider;
            this.points_provider = _provider;

        }
       
        public IActionResult NoOffer()
        {
            _logger.Info(" There is No such Offer To display");
            if (HttpContext.Session.GetString("token") == null)
            {
                
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.Msg = "No Offers Posted By You";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployeeOffers()
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                List<OfferData> offer = new List<OfferData>();
                PointsData points = new PointsData();
                EmployeeData employee = new EmployeeData();
               
                employee.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));
               

                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.ViewEmployeeOffers(employee.EmployeeId, token);
                    var response2 = await points_provider.GetPointsByEmployeeId(employee.EmployeeId, token);

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        var JsonContent2 = await response2.Content.ReadAsStringAsync();

                        offer = JsonConvert.DeserializeObject<List<OfferData>>(JsonContent);
                        
                        points = JsonConvert.DeserializeObject<PointsData>(JsonContent2);

                        ViewBag.Message = "Your Total Points: " + points.TotalPoints;
                        employee.TotalPoints = points.TotalPoints;
                        return View(offer);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "No any record Found! Bad Request";
                        return RedirectToAction("NoOffer");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "Having server issue while adding record";
                        return RedirectToAction("NoOffer");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "No Offers found for Employee :" + employee.EmployeeId;
                        return RedirectToAction("NoOffer");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View(offer.AsEnumerable());

            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewEmployeeProfile()
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                
                PointsData points = new PointsData();
                List<EmployeeData> employeeList = new List<EmployeeData>();
                EmployeeData employee = new EmployeeData();

                employee.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));
               
                

                try
                {
                    string token = HttpContext.Session.GetString("token");
                    
                    var response = await points_provider.GetPointsByEmployeeId(employee.EmployeeId, token);
                    var response2 = await _provider.GetEmployeeList( token);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        var JsonContent2 = await response2.Content.ReadAsStringAsync();
                        points = JsonConvert.DeserializeObject<PointsData>(JsonContent);

                        employeeList = JsonConvert.DeserializeObject<List<EmployeeData>>(JsonContent2);

                        employee.TotalPoints = points.TotalPoints;

                        EmployeeData e = employeeList.FirstOrDefault(c=>c.EmployeeId==employee.EmployeeId);

                        employee.EmployeeName = e.EmployeeName;

                        ViewBag.Message = $" {employee.EmployeeName} Profile";

                        return View(employee);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "No any record Found! Bad Request";
                        return RedirectToAction("NoOffer");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "Having server issue while adding record";
                        return RedirectToAction("NoOffer");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "No Offers found for Employee :" + employee.EmployeeId;
                        return RedirectToAction("NoOffer");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View(employee);

            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewMostLikedOffers()
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                List<OfferData> offer = new List<OfferData>();
                EmployeeData employee = new EmployeeData();
                employee.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));


                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.ViewMostLikedOffers(employee.EmployeeId, token);

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        offer = JsonConvert.DeserializeObject<List<OfferData>>(JsonContent);
                        return View(offer);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "No any record Found! Bad Request";
                        return RedirectToAction("NoOffer");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        ViewBag.Message = "Having server issue while adding record";
                        return RedirectToAction("NoOffer");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.Message = "No Offers found for Employee :" + employee.EmployeeId;
                        return RedirectToAction("NoOffer");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View(offer.AsEnumerable());

            }
        }
    }
}
