using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfferMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OfferMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OfferController : ControllerBase
    {
        private static List<Offer> offers = new List<Offer>
            {
                 new Offer { EmployeeId=101,OfferId = 1, Status = "Available", Likes = 10, Category = "Electronics", OpenedDate =new DateTime(2021,05,01), Details="Resale of Mobile",ClosedDate=new DateTime(),EngagedDate=new DateTime()},

                 new Offer { EmployeeId=102,OfferId = 2, Status = "Engaged", Likes = 55, Category = "Electronics", OpenedDate = new DateTime(2021,05,04),ClosedDate=new DateTime() , Details="Resale of washing machine",EngagedDate=new DateTime(2021,05,08)},

                 new Offer { EmployeeId=103,OfferId = 3, Status = "Engaged", Likes = 20, Category = "Pets", OpenedDate = new DateTime(2021,05,04),ClosedDate=new DateTime() , Details="Golden Retriever for Adoption",EngagedDate=new DateTime(2021,05,09)},

                 new Offer { EmployeeId=103,OfferId = 4, Status = "Available", Likes = 25, Category = "Electronics", OpenedDate = new DateTime(2021,05,09),ClosedDate=new DateTime() , Details="Resale of Mobile",EngagedDate=new DateTime()},

                 new Offer { EmployeeId=103,OfferId = 5, Status = "Available", Likes = 10, Category = "Electronics", OpenedDate = new DateTime(2021,05,09),ClosedDate=new DateTime() , Details="Resale of Laptop",EngagedDate=new DateTime()},

                 new Offer { EmployeeId=103,OfferId = 6 ,Status = "Closed", Likes = 24, Category = "Books", OpenedDate = new DateTime(2021,05,09),EngagedDate=new  DateTime(2021,05,09), ClosedDate=new DateTime(2021,05,10),Details="Wings Of Fire"},

                 new Offer {EmployeeId=104,OfferId = 7, Status = "Available", Likes = 25, Category = "Pets", OpenedDate =new DateTime(2021,05,18), Details="Schitzu for Adoption",ClosedDate=new DateTime(),EngagedDate=new DateTime()},

                 new Offer { EmployeeId=105,OfferId = 8, Status = "Engaged", Likes = 22, Category = "Electronics", OpenedDate = new DateTime(2021,04,04),ClosedDate=new DateTime() , Details="Resale of Mobile",EngagedDate=new DateTime(2021,04,06)},


                 new Offer { EmployeeId=105,OfferId = 9, Status = "Closed", Likes = 18, Category = "Books", OpenedDate = new DateTime(2021,05,01),EngagedDate=new  DateTime(2021,05,03), ClosedDate=new DateTime(2021,05,05),Details="Harry Potter Books"},

            };




        // GET: api/<OfferController>
        [HttpGet]
        [Route("GetOffersList")]
        public IActionResult GetOffersList()
        {
            return Ok(offers);
        }
        

        // GET api/<OfferController>/5
        [HttpGet]
        [Route("GetOfferById/{id}")]
        public IActionResult GetOfferById(int id)
        {

            var offer = offers.FirstOrDefault(c => c.OfferId == id);
            if (offer == null)
            {
                return NotFound("The ID is Invalid");
            }

            return Ok(offer);

        }

        // POST api/<OfferController>
        [HttpPost]
        [Route("PostOffer")]
        public IActionResult PostOffer(Offer newOffer)
        {
            if (newOffer.OfferId == 0 || newOffer.EmployeeId == 0 || newOffer.Category == null || newOffer.Details == null)

            {
                return NotFound();
            }
            else
            {
                offers.Add(newOffer);
            }

            return Ok(offers);
        }

        // PUT api/<OfferController>/5
        [HttpPost]
        [Route("EditOffer")]

        public IActionResult EditOffer(Offer updatedOffer)

        {

            Offer offer = offers.FirstOrDefault(c => c.OfferId == updatedOffer.OfferId && c.EmployeeId == updatedOffer.EmployeeId);
            if (offer == null)
            {
                return NotFound("Offer not found");
            }



            offer.ClosedDate = updatedOffer.ClosedDate;

            offer.Status = updatedOffer.Status;

            offer.Details = updatedOffer.Details;

            offer.Category = updatedOffer.Category;

            if (offer.ClosedDate > offer.EngagedDate && offer.Status != "Closed")
            {
                return BadRequest("Please update status to Closed");
            }

            return Ok("Edited Successfully");

            //return offers;

        }




        [HttpGet]
        [Route("GetOfferByCategory/{category}")]
        public IActionResult GetOfferByCategory(string category)
        {
            var offer = offers.Where(e => e.Category == category).ToList();

            if (offer.Count == 0)
            {
                return NotFound("The Category is not found in the Offer List");
            }
            return Ok(offer); // results in 200 ok status 

        }

        [HttpGet]
        [Route("GetOfferByOpenedDate/{openedDate}")]
        public IActionResult GetOfferByOpenedDate(string openedDate)
        {
            var offer = offers.Where(e => e.OpenedDate.ToString().Substring(0, 10) == openedDate).ToList();

            // var offer = from c in offers where c.OpenedDate.ToString("dd-MM-yyyyy") == openedDate select c;
            if (offer.Count == 0)
            {
                return NotFound("The Engaged Date is not found");
            }
            return Ok(offer); // results in 200 ok status 

        }


        [HttpGet]
        [Route("GetOfferByTopThreeLikes/{category}")]
        public IActionResult GetOfferByTopThreeLikes(string category)
        {

            var offer = (from c in offers where c.Category == category orderby c.Likes descending select c).Take(3);
            if (offer.Count() == 0)
            {
                return NotFound("The Category searched is not found in the offer list");
            }
            return Ok(offer); // results in 200 ok status 

        }

        [HttpPost]
        [Route("EngageOffer")]
        public IActionResult EngageOffer(Offer offerDetails)
        {
            
            Offer offer = offers.FirstOrDefault(c => c.OfferId == offerDetails.OfferId && c.EmployeeId == offerDetails.EmployeeId);
            if (offer == null)
            {
                return NotFound("Offer not found");
            }
            else if (offer.Status == "Engaged" || offer.Status == "Closed")
            {

                return BadRequest("Offer is either Engaged or Closed");
            }

            else
            {

                //Note : Display Status as Engaged in ViewBag in Views

                offer.Status = "Engaged";
                offer.EngagedDate = DateTime.Now;
                return Ok("Offer status updated to Engaged");
                //return offers;
            }

        }

        [HttpPost]
        [Route("LikeOffer")]
        public IActionResult LikeOffer(Offer o)
        {


            // Like like = new Like();

            Offer offer = offers.FirstOrDefault(c => c.OfferId == o.OfferId);
            if (offer == null)
            {
                return NotFound("Offer not found");
            }

            else
            {

                //Note : Display Status as Engaged in ViewBag in Views

                offer.Likes = offer.Likes + 1;
                
                return Ok("Liked");
                //return offers;
            }

        }
      

    }
}