//using CLassifiedsUIPortal.Models;
//using CLassifiedsUIPortal.Provider;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CLassifiedsUIPortal.Controllers
//{
//    public class HomeController : Controller
//    {

//        OfferHelper _api = new OfferHelper();

//        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(OfferController));
//        private readonly IOfferProvider _provider;
//        public HomeController(IOfferProvider provider)
//        {
//            this._provider = provider;
//        }


//        public async Task<IActionResult> GetOffersList()
//        {

//            if (HttpContext.Session.GetString("token") == null)
//            {
//                return RedirectToAction("Login", "Auth");
//            }
//            else
//            {
//                List<OfferData> offer = new List<OfferData>();


//                try
//                {
//                    string token = HttpContext.Session.GetString("token");
//                    var response = await _provider.GetOffersList(token);

//                    if (response.IsSuccessStatusCode)
//                    {
//                        var JsonContent = await response.Content.ReadAsStringAsync();
//                        offer = JsonConvert.DeserializeObject<List<OfferData>>(JsonContent);
//                        return View(offer);
//                    }
//                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
//                    {
//                        ViewBag.Message = "No any record Found! Bad Request";
//                        return RedirectToAction("NoOffer");
//                    }
//                    else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
//                    {
//                        ViewBag.Message = "Having server issue while adding record";
//                        return RedirectToAction("NoOffer");
//                    }
//                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
//                    {
//                        ViewBag.Message = "No record found in DB for Category :";
//                        return RedirectToAction("NoOffer");
//                    }
//                }
//                catch (Exception e)
//                {
//                    _logger.Error("Exception occured as :" + e.Message);
//                }
//                return View(offer.AsEnumerable());
//            }
//        }



//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }

//       /* public ActionResult Like(int offerId)
//        {

//            if (HttpContext.Session.GetString("token") == null)
//            {
//                return RedirectToAction("Login", "Auth");
//            }

//            return View();
//        }*/

//        public IActionResult LikeOffer()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> LikeOffer(int offerId)
//        {
//            //string sd = openedDate.ToString("dd-MM-yyyyy");



//            if (HttpContext.Session.GetString("token") == null)
//            {
//                return RedirectToAction("Login", "Auth");

//            }
//            else
//            {
//                OfferData offerDetails = new OfferData();

//                offerDetails.OfferId = offerId;

//                offerDetails.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));


//                List<OfferData> offersUpdatedList = new List<OfferData>();

//                try
//                {
//                    string token = HttpContext.Session.GetString("token");
//                    var response = await _provider.LikeOffer(offerDetails, token);
//                    if (response.IsSuccessStatusCode)
//                    {
//                        ViewBag.Message = $" liked";

//                    }

//                   /* if (response.IsSuccessStatusCode)
//                    {
//                        var JsonContent = await response.Content.ReadAsStringAsync();
//                        offersUpdatedList = JsonConvert.DeserializeObject<List<OfferData>>(JsonContent);
//                        return View(offersUpdatedList);
//                    }

//                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
//                    {
//                        ViewBag.Message = $"Not liked";

//                    }

//                    else
//                    {
//                        ViewBag.message = "You are not authorized to engage this offer: " + offerId;

//                    }
//                    */

//                }
//                catch (Exception e)
//                {
//                    _logger.Error("Exception occured as :" + e.Message);
//                }

//                return View(offersUpdatedList.AsEnumerable());
//            }
//        }
//    }
//}

using CLassifiedsUIPortal.Models;
using CLassifiedsUIPortal.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CLassifiedsUIPortal.Controllers
{
    public class HomeController : Controller
    {
        public static List<OfferData> offer = new List<OfferData>();

        OfferHelper _api = new OfferHelper();

        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(OfferController));
        private readonly IOfferProvider _provider;
        public HomeController(IOfferProvider provider)
        {
            this._provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOffersList()
        {

            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {



                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.GetOffersList(token);

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = await response.Content.ReadAsStringAsync();
                        var newOfferlen = JsonConvert.DeserializeObject<List<OfferData>>(JsonContent).Count;
                        if (offer.Count != newOfferlen)
                        {
                            offer = JsonConvert.DeserializeObject<List<OfferData>>(JsonContent);
                        }
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
                        ViewBag.Message = "No record found in DB for Category :";
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /* public ActionResult Like(int offerId)
         {

             if (HttpContext.Session.GetString("token") == null)
             {
                 return RedirectToAction("Login", "Auth");
             }

             return View();
         }*/

        public IActionResult LikeOffer(int offerId)
        {
            foreach (var items in offer)
            {
                if (items.OfferId == offerId)
                {
                    items.Likes++;
                }
            }
            return View("GetOffersList", offer);
        }

        [HttpPost]
        public async Task<IActionResult> LikeOffertest(int offerId)
        {
            //string sd = openedDate.ToString("dd-MM-yyyyy");




            if (HttpContext.Session.GetString("token") == null)
            {
                return RedirectToAction("Login", "Auth");

            }
            else
            {
                OfferData offerDetails = new OfferData();

                offerDetails.OfferId = offerId;

                offerDetails.EmployeeId = Convert.ToInt32(HttpContext.Session.GetInt32("EmployeeId"));


                List<OfferData> offersUpdatedList = new List<OfferData>();

                try
                {
                    string token = HttpContext.Session.GetString("token");
                    var response = await _provider.LikeOffer(offerDetails, token);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.Message = $" liked";

                    }

                    /* if (response.IsSuccessStatusCode)
                     {
                         var JsonContent = await response.Content.ReadAsStringAsync();
                         offersUpdatedList = JsonConvert.DeserializeObject<List<OfferData>>(JsonContent);
                         return View(offersUpdatedList);
                     }

                     else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                     {
                         ViewBag.Message = $"Not liked";

                     }

                     else
                     {
                         ViewBag.message = "You are not authorized to engage this offer: " + offerId;

                     }
                     */

                }
                catch (Exception e)
                {
                    _logger.Error("Exception occured as :" + e.Message);
                }

                return View(offersUpdatedList.AsEnumerable());
            }
        }
    }
}

