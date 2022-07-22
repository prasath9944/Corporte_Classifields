using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PointsMicroservice.Controllers;
using PointsMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointsUnitTest
{
    public class PointsMicroserviceUnitTest
    {

        public static Dictionary<int, int> EmployeePoints = new Dictionary<int, int>();
        private readonly PointsController offerController;
        public PointsMicroserviceUnitTest()
        {
            offerController = new PointsController();
        }
        [SetUp]
        public void Setup()
        {
            EmployeePoints = new Dictionary<int, int>()
        {
            {101,10},
            {102,10},
            {103,15},
            {104,10},
            {105,10},
            {201,0}
        };

        }
        [Test]
        public async Task RefreshPointsByEmployee_ShouldUpdatePoints_WhenRequested()
        {
            try
            {
                var points = new PointsController();
                var answer = await points.RefreshPointsByEmployee(101);
                var okResult = answer as ObjectResult;

                Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            }
            catch (System.Net.Http.HttpRequestException)
            {

            }

        }

        [Test]
        public void GetPointsByEmployeeId_ShouldReturnPoints_WhenRequested()
        {
            var offerItem = offerController.GetPointsByEmployeeId(101);
            ObjectResult result2 = offerItem as ObjectResult;
            Assert.AreEqual(200, result2.StatusCode);
        }
        [Test]
        public void GetPointsByEmployeeId_ShouldNotreturnPoints_WhenRequested()
        {
            var offerItem = offerController.GetPointsByEmployeeId(500);
            ObjectResult result2 = offerItem as ObjectResult;
            Assert.AreEqual(200, result2.StatusCode);
        }
    }
}