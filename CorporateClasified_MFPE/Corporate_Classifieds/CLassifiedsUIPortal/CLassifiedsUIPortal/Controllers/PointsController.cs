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
    public class PointsController : Controller
    {

        PointsHelper _api = new PointsHelper();

        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(EmployeeController));
        private readonly IPointsProvider _provider;
        public PointsController(IPointsProvider provider)
        {
            this._provider = provider;
        }
     



        [HttpGet]
        public async Task<ActionResult> GetPointsByEmployeeId()
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                var points = new PointsData();
               
                
                points.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));


                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetPointsByEmployeeId(points.EmployeeId, token);
                   

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        points = JsonConvert.DeserializeObject<PointsData>(JsonContent);
                        return View(points);
                    }
                    /*
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "No any record Found! Bad Request";
                        return RedirectToAction("NoEmployee");
                    }
                
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        //ViewBag.Message = "No Offers found for Employee :" + employee.EmployeeId;
                        return RedirectToAction("NoEmployee");
                    }
                    */
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View(points);

            }
        }

        [HttpGet]
        public async Task<ActionResult> RefreshPointsByEmployee()
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                var points = new PointsData();


                points.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));


                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.RefreshPointsByEmployee(points.EmployeeId, token);


                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        points = JsonConvert.DeserializeObject<PointsData>(JsonContent);
                        ViewBag.Message = "Points Updated";
                        return View(points);
                    }
                    /*
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "No any record Found! Bad Request";
                        return RedirectToAction("NoEmployee");
                    }
                
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        //ViewBag.Message = "No Offers found for Employee :" + employee.EmployeeId;
                        return RedirectToAction("NoEmployee");
                    }
                    */
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View(points);

            }
        }
       

        }
}
