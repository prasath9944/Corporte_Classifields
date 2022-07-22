using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PointsMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointsMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        Helper _api = new Helper();



        public static Dictionary<int, int> EmployeePoints = new Dictionary<int, int>()
        {
            {101,10},
            {102,10},
            {103,15},
            {104,10},
            {105,10},
            {201,0}
        };

        /*
           public List<KeyValuePair<int, int>> EmployeePoints = new List<KeyValuePair<int, int>>()
           {
               new KeyValuePair<int, int>(101,10),
                new KeyValuePair<int, int>(102,10),
                 new KeyValuePair<int, int>(103,15),
                  new KeyValuePair<int, int>(104,10),
                   new KeyValuePair<int, int>(105,0),

           };

        */





        public static List<Like> Likes = new List<Like>()
            {
                new Like {OfferId=1,LikeDate= new DateTime(2021, 05, 01)},
                new Like {OfferId =1, LikeDate = new DateTime(2021, 05, 01)},
                new Like { OfferId = 1, LikeDate = new DateTime(2021, 05, 02) },

                new Like { OfferId = 2, LikeDate = new DateTime(2021, 05, 01) },
                new Like { OfferId = 2, LikeDate = new DateTime(2021, 05, 02) },
                new Like { OfferId = 2, LikeDate = new DateTime(2021, 05, 01) },
                new Like { OfferId = 2, LikeDate = new DateTime(2021, 05, 01) },
                new Like { OfferId = 2, LikeDate = new DateTime(2021, 05, 02) },
                new Like { OfferId = 2, LikeDate = new DateTime(2021, 05, 01) },

                new Like {OfferId=3,LikeDate= new DateTime(2021, 05, 04)},
                new Like {OfferId =3, LikeDate = new DateTime(2021, 05, 05)},
                new Like { OfferId = 3, LikeDate = new DateTime(2021, 05, 05) },
                new Like { OfferId = 3, LikeDate = new DateTime(2021, 05, 06) },

                new Like { OfferId = 4, LikeDate = new DateTime(2021, 05, 09) },
                new Like { OfferId = 4, LikeDate = new DateTime(2021, 05, 10) },
                new Like { OfferId = 4, LikeDate = new DateTime(2021, 05, 10) },

                new Like { OfferId = 5, LikeDate = new DateTime(2021, 05, 10) },
                new Like { OfferId = 5, LikeDate = new DateTime(2021, 05, 11) },
                new Like { OfferId = 5, LikeDate = new DateTime(2021, 05, 11) },
                new Like { OfferId = 6, LikeDate = new DateTime(2021, 05, 12) },

            };

       

        [HttpGet]
        [Route("RefreshPointsByEmployee/{employeeId}")]
        public async Task<ActionResult> RefreshPointsByEmployee(int employeeId)
        {

            int id, likes_two_days, totalPoints = 0;

            Points points = new Points();
            
            DateTime date;

            List<OfferData> newOffers = await GetList();

            var employeeoffer = newOffers.Where(c => c.EmployeeId == employeeId).ToList();
            foreach (var e in employeeoffer)
            {
                id = e.OfferId;
                date = e.OpenedDate;
                likes_two_days = GetLikesInTwoDays(id, date);
                e.LikesInTwoDays = likes_two_days;
            }

            foreach (var e in employeeoffer)
            {
                TimeSpan engaggedDuration = e.EngagedDate - e.OpenedDate;
                if (e.LikesInTwoDays > 2)
                    totalPoints += 25;
                else if (e.LikesInTwoDays > 4)
                    totalPoints += 50;
                else if (e.Status == "Engaged" && engaggedDuration.TotalDays <= 2)
                {
                    totalPoints += 100;
                }
            }

            EmployeePoints[employeeId] += totalPoints;
            //EmployeePoints.Add(employeeId,totalPoints);

            //  return totalPoints;

            points.EmployeeId = employeeId;
            points.TotalPoints = EmployeePoints[employeeId];

            //return Ok("Points updated");
            return Ok(points);

            //return EmployeePoints[employeeId];
        }

        [HttpGet]
        [Route("GetPointsByEmployeeId/{employeeId}")]
        public ActionResult GetPointsByEmployeeId(int employeeId)
        {
            int p;
            Points point = new Points();
                for (int i = 0; i < EmployeePoints.Count; i++)
                {

                    if (EmployeePoints.ElementAt(i).Key == employeeId)
                    {
                        p = EmployeePoints.ElementAt(i).Value;

                    point.TotalPoints = p;
                    point.EmployeeId = employeeId;
                    return Ok(point);
                    }
                }   
            return Ok(0);
        }




        [HttpGet]
        [Route("GetList")]
        public async Task<List<OfferData>> GetList()
        {
            List<OfferData> offers = new List<OfferData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/offer/GetOffersList");

           

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                offers = JsonConvert.DeserializeObject<List<OfferData>>(results);
            }

            //List<OfferDto> lists = offers.ToList();
            return offers;
        }

        [HttpGet]
        [Route("GetLikeIn2Days/{id}/{date}")]
        public  int GetLikesInTwoDays(int id, DateTime date)
        {
           // List<OfferProvider> newOffers = await GetList();
            int count = Likes.Where(c => c.LikeDate == date && c.OfferId == id).Count();

            int count1 = Likes.Where(c => c.LikeDate == date.AddDays(1) && c.OfferId == id).Count();

            // int count2 = Likes.Where(c => c.LikeDate == date.AddDays(2) && c.OfferId == id).Count();
            int totalLikes = count + count1;

            return totalLikes;

        }


   
    }
}
