using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuth.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace JWTAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet("VP")]
        [Authorize(Roles ="VP")]
        public IActionResult VPEndpoint()
        {
            var currentUser = GetCurrentUser();
            if(currentUser!=null)
            {
                return Ok($"Hi,The user is {currentUser.GivenName}, and the role is  {currentUser.Role}");
            }
            return NotFound("");
            
        }

        [HttpGet("AVP")]
        [Authorize]
        public IActionResult AVPEndpoint()
        {
            var currentUser = GetCurrentUser();
            if (currentUser != null)
            {
                return Ok($"Hi,The user is {currentUser.GivenName}, and the role is  {currentUser.Role}");
            }
            return NotFound("");

        }


        private UserModel GetCurrentUser()
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity!=null)
            {
                var userClaims = identity.Claims;

                return new UserModel()
                {
                    UserName = userClaims.FirstOrDefault(r => r.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress= userClaims.FirstOrDefault(r => r.Type == ClaimTypes.Email)?.Value,
                    GivenName= userClaims.FirstOrDefault(r => r.Type == ClaimTypes.GivenName)?.Value,
                    Surname= userClaims.FirstOrDefault(r => r.Type == ClaimTypes.Surname)?.Value,
                    Role= userClaims.FirstOrDefault(r => r.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }

  
}
