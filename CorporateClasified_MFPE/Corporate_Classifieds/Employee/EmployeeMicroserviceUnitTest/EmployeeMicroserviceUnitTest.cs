using EmployeeMicroservice.Controller;
using EmployeeMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeMicroserviceUnitTest
{
    public class EmployeeMicroserviceUnitTest
    {
        EmployeeController employeeController = new EmployeeController();
        List<Employee> employees;
        [SetUp]
        public void Setup()
        {
            employees = new List<Employee>()
            {
                new Employee{EmployeeId=101, EmployeeName="Lakshmi Sruthi", Password="12345"},
                new Employee{EmployeeId=102, EmployeeName="Sindhu", Password="12345"},
                new Employee{EmployeeId=103, EmployeeName="Vijay", Password="12345"},
                new Employee{EmployeeId=104, EmployeeName="Harish", Password="12345"},
                new Employee{EmployeeId=105, EmployeeName="Chinmayee", Password="12345"},
                new Employee{EmployeeId=201, EmployeeName="Jata", Password="12345"},
            };
        }

        [Test]
        public void GetEmployeeList_ShouldReturnAllEmployees_WhenRequested()
        {
            try
            {
                ActionResult actionResult = employeeController.GetEmployeeList();
                ObjectResult result = actionResult as ObjectResult;
                Assert.AreEqual(200, result.StatusCode);
            }
            catch (System.Net.Http.HttpRequestException )
            {

            }
        }
        [Test]
        public async Task ViewEmployeeOffers_ShouldReturnOffer_WhenRequested()
        {
            try
            {
                ActionResult actionResult = await employeeController.ViewEmployeeOffers(101);
                ObjectResult result = actionResult as ObjectResult;
                Assert.AreEqual(200, result.StatusCode);
            }
            catch (System.Net.Http.HttpRequestException )
            {

            }
        }
        [Test]
        public async Task ViewMostLikedOffers_ShouldReturnMostLikedOffer_WhenRequested()
        {
            try
            {
                ActionResult actionResult = await employeeController.ViewMostLikedOffers(101);
                ObjectResult objectResult = actionResult as ObjectResult;
                Assert.AreEqual(200, objectResult.StatusCode);
            }
            catch (System.Net.Http.HttpRequestException )
            {

            }
        }
        [Test]
        public async Task GetPointsList_ShouldReturnPoints_WhenRequested()
        {
            try
            {
                ActionResult actionResult = await employeeController.GetPointsList();
                ObjectResult objectResult = actionResult as ObjectResult;
                Assert.AreEqual(200, objectResult.StatusCode);
            }
            catch (System.Net.Http.HttpRequestException )
            {

            }
        }
    }
}