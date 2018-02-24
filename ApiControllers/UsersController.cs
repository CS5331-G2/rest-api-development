using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.ApiModels.UsersController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diary.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        // POST /api/users/register OR :8080/users/register
        [HttpPost]
        [Route("register")]
        public RegisterUserResponse Register(RegisterUserRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                return new RegisterUserResponse()
                {
                    Status = true
                };
            }
            
            return new RegisterUserResponse()
            {
                Status = false,
                Error = "Invalid model"
            };
        }

        // POST /api/users/authenticate OR :8080/users/authenticate
        [HttpPost]
        [Route("authenticate")]
        public AuthenticateUserResponse Authenticate(AuthenticateUserRequest authenticationRequest)
        {
            if (ModelState.IsValid)
            {
                return new AuthenticateUserResponse()
                {
                    Status = true,
                    Token = Guid.NewGuid().ToString("D")
                };
            }
            
            return new AuthenticateUserResponse()
            {
                Status = false
            };
        }

        // POST /api/users/expire OR :8080/users/expire
        [HttpPost]
        [Route("expire")]
        public ExpireUserResponse Expire(ExpireUserRequest expireRequest)
        {
            if (ModelState.IsValid)
            {
                return new ExpireUserResponse()
                {
                    Status = true
                };
            }

            return new ExpireUserResponse()
            {
                Status = false
            };
        }

        // POST /api/users OR :8080/users
        [HttpPost]
        public RetrieveUserResponse Post(RetrieveUserRequest retrieveRequest)
        {
            if (ModelState.IsValid)
            {
                string username = "User" + new Random().Next(1, 100);
                return new RetrieveUserResponse()
                {
                    Status = true,
                    Username = username,
                    Fullname = username + "'s Full Name",
                    Age = new Random().Next(21,30)
                };
            }

            return new RetrieveUserResponse()
            {
                Status = false,
                Error = "Invalid model"
            };
        }
    }
}
