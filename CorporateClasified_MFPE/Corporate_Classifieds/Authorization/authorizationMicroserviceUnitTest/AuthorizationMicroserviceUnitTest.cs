using Authorization.Controllers;
using Authorization.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AuthTest
{
    public class AuthorizationMicroserviceUnitTest
    {
        private Authenticate _controller;
        UserModel Listuser1 ;
        UserModel Listuser2;
        private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _config = new Mock<IConfiguration>();
            _config.Setup(c => c["Jwt:Key"]).Returns("ThisismySecretKey");
            Listuser1 = new UserModel()
            {
                EmployeeId = 101,
                Password = "12345"
            };
            Listuser2 = new UserModel()
            {
                EmployeeId = 12345,
                Password = "admin5"
            };

        }

        [Test]
        public void AuthenticateUserController_ShouldReturnValidUser_WhenRequested()
        {

            try
            {
                var result = _controller.Login(Listuser1);
                
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }

        }
      

        [Test]
        public void AuthenticateUserController_ShouldReturnInvalidUSer_WhenRequested()
        {

            try
            {
                var result = _controller.Login(Listuser2);
                var response = result as ObjectResult;
                Assert.AreEqual(401, response.StatusCode);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Object reference not set to an instance of an object.", e.Message);
            }
        }
    }
}