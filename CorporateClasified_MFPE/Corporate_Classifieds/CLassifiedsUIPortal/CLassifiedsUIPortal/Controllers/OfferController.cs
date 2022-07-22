using CLassifiedsUIPortal.Models;
using CLassifiedsUIPortal.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Controllers
{
    public class OfferController : Controller
    {
        // GET: EmployeeController
        OfferHelper _api = new OfferHelper();

        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(OfferController));
        private readonly IOfferProvider _provider;
        public OfferController(IOfferProvider provider)
        {
            this._provider = provider;
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
        public ActionResult GetId(int Id)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        public IActionResult NoOffer()
        {
            _logger.Info(" There is No such Product To display");
            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetOfferById(int Id)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                var offer = new OfferData();


                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetOfferById(Id, token);

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        offer = JsonConvert.DeserializeObject<OfferData>(JsonContent);
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
                        ViewBag.Message = "No record found in DB for ID :" + Id;
                        return RedirectToAction("NoOffer");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View(offer);
            }
        }

        /*
        var offers = new OfferData();
        HttpClient client = _api.Initial();
        HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/offer/GetOfferById/{Id}");
        if(httpResponseMessage.IsSuccessStatusCode)
        {
            var results = httpResponseMessage.Content.ReadAsStringAsync().Result;
            offers = JsonConvert.DeserializeObject<OfferData>(results);
        }

        return View(offers);
        */

        public ActionResult GetCategory(string category)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetOfferByCategory(string category)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                List<OfferData> offer = new List<OfferData>();


                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetOfferByCategory(category, token);

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
                        ViewBag.Message = "No record found in DB for Category :" + category;
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


        public ActionResult GetTopThreeLikes(string category)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetOfferByTopThreeLikes(string category)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                List<OfferData> offer = new List<OfferData>();


                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetOfferByTopThreeLikes(category, token);

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
                        ViewBag.Message = "No record found in DB for Category :" + category;
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

        public ActionResult GetByOpenedDate(DateTime openedDate)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetOfferByOpenedDate(DateTime openedDate)
        {
            string sd = openedDate.ToString("dd-MM-yyyyy");


            //DateTime sd1 = DateTime.Parse(sd);

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                List<OfferData> offer = new List<OfferData>();


                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetOfferByOpenedDate(sd, token);

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
                        ViewBag.Message = "No record found in DB for Category :" + openedDate;
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


        public ActionResult PostOffer()
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostOffer(OfferData offer)
        {
            //string sd = openedDate.ToString("dd-MM-yyyyy");

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");

            }
            else
            {
                List<OfferData> offerDetails = new List<OfferData>();

                offer.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.PostOffer(offer, token);

                    if (response.IsSuccessStatusCode)
                    {


                        ViewBag.message = "Your offer is posted successfully!";

                    }
                    else
                    {
                        ViewBag.message = "Fill All Details Properly";

                    }


                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }

                return View(offer);
            }
        }
        public ActionResult EngageOffer()
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EngageOffer(int offerId)
        {
            //string sd = openedDate.ToString("dd-MM-yyyyy");



            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");

            }
            else
            {
                OfferData offerDetails = new OfferData();
                // offerDetails.EmployeeId = employeeId;
                offerDetails.OfferId = offerId;

                offerDetails.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));

                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.EngageOffer(offerDetails, token);

                    if (response.IsSuccessStatusCode)
                    {

                        ViewBag.message = $"Your offer {offerId} is engaged successfully!";

                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = $"Offer {offerId} is already Engaged or Closed";

                    }

                    else
                    {
                        ViewBag.message = "You are not authorized to engage this offer: " + offerId;

                    }


                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }

                return View();
            }
        }


        public ActionResult GetIdForEditOffer(int Id)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetOfferByIdForEdit(int offerId)
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                var offer = new OfferData();


                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetOfferById(offerId, token);

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        offer = JsonConvert.DeserializeObject<OfferData>(JsonContent);
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
                        ViewBag.Message = "No record found in DB for ID :" + offerId;
                        return RedirectToAction("NoOffer");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }
                return View(offer);

            }
        }

        public ActionResult EditOffer()
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditOffer(OfferData offer)
        {
            //string sd = openedDate.ToString("dd-MM-yyyyy");

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");

            }
            else
            {
              
                offer.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));
                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.EditOffer(offer, token);

                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Your offer is edited successfully!";

                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Message = "Please update status to Closed";
                      
                    }
                    else
                    {
                        ViewBag.Message = "Fill All Details Properly";

                    }


                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }

                return View(offer);
            }
        }

    }
}
