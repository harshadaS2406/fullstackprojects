using JWTAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JWTAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private IConfiguration _config;

        // injecting IConfig object to access all the details from appsettings.json 
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if(user!=null)
            {

                var token = GenerateToken(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }


        public UserModel Authenticate(UserLogin userLogin)
        {
            var result = UserConstants.lstUsers.FirstOrDefault(r=>r.UserName.ToLower()==userLogin.UserName.ToLower() && r.Password== userLogin.Password);
            if (result != null)
            {
                return result;
            }
            else
                return null;
        }
        public string GenerateToken(UserModel userModel)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claimsDetail = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userModel.UserName),
                new Claim(ClaimTypes.Email,userModel.EmailAddress),
                new Claim(ClaimTypes.GivenName,userModel.GivenName),
                new Claim(ClaimTypes.Surname,userModel.Surname),
                new Claim(ClaimTypes.Role,userModel.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claimsDetail,
                expires: System.DateTime.Now.AddMinutes(15),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
