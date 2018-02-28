using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.ApiModels;
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
        public ApiResponseModel Register(RegisterUserRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                // TODO: Perform registration after checking username not taken
                Response.StatusCode = 201;
                return new ApiResponseModel()
                {
                    Status = true
                };
            }
            
            return new ApiResponseModel()
            {
                Status = false,
                Error = "User already exists!"
            };
        }

        // POST /api/users/authenticate OR :8080/users/authenticate
        [HttpPost]
        [Route("authenticate")]
        public ApiResponseModel Authenticate(AuthenticateUserRequest authenticationRequest)
        {
            if (ModelState.IsValid)
            {
                // TODO: perform authentication
                return new ApiResponseModel()
                {
                    Status = true,
                    Result = new AuthenticateUserResultModel()
                    {
                        Token = Guid.NewGuid().ToString("D")
                    }
                };
            }
            
            return new ApiResponseModel()
            {
                Status = false
            };
        }

        // POST /api/users/expire OR :8080/users/expire
        [HttpPost]
        [Route("expire")]
        public ApiResponseModel Expire(ExpireUserRequest expireRequest)
        {
            if (ModelState.IsValid)
            {
                // TODO: Find uuidv4 token belonging to an user and set it to null to expire it.
                return new ApiResponseModel()
                {
                    Status = true
                };
            }

            return new ApiResponseModel()
            {
                Status = false
            };
        }

        // POST /api/users OR :8080/users
        [HttpPost]
        public ApiResponseModel Post(RetrieveUserRequest retrieveRequest)
        {
            if (ModelState.IsValid)
            {
                // TODO: with the uuidv4 token, find the user, then return informaion if user exists
                string username = "User" + new Random().Next(1, 100);
                return new ApiResponseModel()
                {
                    Status = true,
                    Result = new RetrieveUserResultModel()
                    {
                        Username = username,
                        Fullname = username + "'s Full Name",
                        Age = new Random().Next(21,30)
                    }
                };
            }

            return new ApiResponseModel()
            {
                Status = false,
                Error = "Invalid authentication token."
            };
        }
    }
}
