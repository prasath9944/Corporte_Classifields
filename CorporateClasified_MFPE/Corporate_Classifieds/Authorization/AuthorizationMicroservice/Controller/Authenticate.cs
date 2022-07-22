using Authorization.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class Authenticate : ControllerBase
    {
        private IConfiguration _config;

        public Authenticate(IConfiguration config)
        {
            _config = config;
        }
        // GET: api/<Authenticate>


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

      

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = Listuser.FirstOrDefault(z => z.EmployeeId == login.EmployeeId && z.Password == login.Password);
            return user;
        }
        public List<UserModel> Listuser = new List<UserModel>()
        {

        new UserModel{EmployeeId=101,Password="12345"},
        new UserModel{EmployeeId=102,Password="12345"},
        new UserModel{EmployeeId=103,Password="12345"},
        new UserModel{EmployeeId=104,Password="12345"},
        new UserModel{EmployeeId=105,Password="12345"},
        new UserModel{EmployeeId=201,Password="12345"},


     };
    }
}

