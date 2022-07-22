using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using OfferMicroservice.Controllers;
using OfferMicroservice.Models;
using System;
using System.Collections.Generic;

namespace OfferUnitTest
{
    [TestFixture]
    public class OfferMicroserviceUnitTest
    {
        List<Offer> offers = new List<Offer>();
        private readonly OfferController offerController;
        public OfferMicroserviceUnitTest()
        {
            offerController=new OfferController();
        }

        [SetUp]
        public void Setup()
        {
            offers = new List<Offer>()
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

        }

        [Test]
        public void GetOfferList_ShouldReturnAllOffers_WhenRequested()
        {
            var offerItem = offerController.GetOffersList();
            ObjectResult result1 = offerItem as ObjectResult;
            Assert.AreEqual(200,result1.StatusCode);
        }
        [Test]
        public void GetOfferById_ShouldRetuenOffer_WhenRequested()
        {
            var offerItem = offerController.GetOfferById(1);
            ObjectResult result2 = offerItem as ObjectResult;
            Assert.AreEqual(200, result2.StatusCode);
        }
        [Test]
        public void GetOfferById_ShouldNotReturnOffer_WhenRequested()
        {
            var offerItem = offerController.GetOfferById(10);
            ObjectResult result3 = offerItem as ObjectResult;
            Assert.AreEqual(404, result3.StatusCode);
        }
        [Test]
        public void GetOfferByCategory_ShouldReturnOffers_WhenRequested()
        {
            var offerItem = offerController.GetOfferByCategory("Electronics");
            ObjectResult result3 = offerItem as ObjectResult;
            Assert.AreEqual(200, result3.StatusCode);
        }
        [Test]
        public void GetOfferByCategory_ShouldNotReturnOffers_WhenRequested()
        {
            var offerItem = offerController.GetOfferByCategory("Iphone");
            ObjectResult result3 = offerItem as ObjectResult;
            Assert.AreEqual(404, result3.StatusCode);
        }
        [Test]
        public void GetOfferByOpenedDate_ShouldReturnOffers_WhenRequested()
        {
            var offerItem = offerController.GetOfferByOpenedDate("09-05-2021");
            ObjectResult result3 = offerItem as ObjectResult;
            Assert.AreEqual(200, result3.StatusCode);

        }
        [Test]
        public void GetOfferByOpenedDate_ShouldNotReturnOffers_WhenRequested()
        {
            var offerItem = offerController.GetOfferByOpenedDate("3021-05-23");
            ObjectResult result3 = offerItem as ObjectResult;
            Assert.AreEqual(404, result3.StatusCode);

        }
        [Test]
        public void GetOfferByTopThreeLikes_ShouldReturnOffers_WhenRequested()
        {
            var offerItem = offerController.GetOfferByTopThreeLikes("Electronics");
            ObjectResult result3 = offerItem as ObjectResult;
            Assert.AreEqual(200, result3.StatusCode);
        }
        [Test]
        public void GetOfferByTopThreeLikes_ShouldNotReturnOffers_WhenRequested()
        {
            var offerItem = offerController.GetOfferByTopThreeLikes("IPhones");
            ObjectResult result3 = offerItem as ObjectResult;
            Assert.AreEqual(404, result3.StatusCode);
        }
    }
}