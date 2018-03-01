using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.ApiModels;
using diary.ApiModels.UsersController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using diary.Models;
using diary.Data;
using System.Security.Cryptography;

namespace diary.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public UsersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST /api/users/register OR :8080/users/register
        [HttpPost]
        [Route("register")]
        public ApiResponseModel Register([FromBody]RegisterUserRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                // TODO: Perform registration after checking username not taken
                //**DONE BY ZIKAI */
                Response.StatusCode = 201;

                IQueryable<ApplicationUser> result = _dbContext.Users.Where(p => p.UserName == registerRequest.Username);
                if (result.Count() > 0)
                {
                    return new ApiResponseModel()
                    {
                        Status = false,
                        Error = "User already exists!"
                    };
                }
                else
                {
                    SHA256 newSHA = SHA256Managed.Create();
                    byte[] hashbytes = newSHA.ComputeHash(System.Text.Encoding.Unicode.GetBytes(registerRequest.Password));


                    ApplicationUser newUser = new ApplicationUser()
                    {
                        UuidV4Token = System.Guid.NewGuid().ToString(),
                        UserName = registerRequest.Username,
                        Fullname = registerRequest.FullName,
                        Age = registerRequest.Age,
                        PasswordHash = System.Text.Encoding.Unicode.GetString(hashbytes)
                    };

                    _dbContext.Add(newUser);

                    if (_dbContext.SaveChanges() > 0)
                    {
                        return new ApiResponseModel()
                        {
                            Status = true
                        };
                    }
                    else
                    {
                        return new ApiResponseModel()
                        {
                            Status = false,
                            Error = "Database Error!"
                        };
                    }
                }


            }

            return new ApiResponseModel()
            {
                Status = false,
                Error = "Error registering user!"
            };

            //**END OF CODE DONE BY ZIKAI */
        }

        // POST /api/users/authenticate OR :8080/users/authenticate
        [HttpPost]
        [Route("authenticate")]
        public ApiResponseModel Authenticate([FromBody]AuthenticateUserRequest authenticationRequest)
        {
            if (ModelState.IsValid)
            {
                // TODO: perform authentication
                //**ZIKAI CODED SOME STUFF */
                SHA256 newSHA = SHA256Managed.Create();
                string user = authenticationRequest.Username;
                string password = System.Text.Encoding.Unicode.GetString(
                    newSHA.ComputeHash(System.Text.Encoding.Unicode.GetBytes(
                        authenticationRequest.Password)));

                IQueryable<ApplicationUser> result = _dbContext.Users.Where(p => p.UserName == user && p.PasswordHash == password);

                if (result.Count() == 1)
                {
                    return new ApiResponseModel()
                    {
                        Status = true,
                        Result = new AuthenticateUserResultModel()
                        {
                            Token = Guid.NewGuid().ToString("D")
                        }
                    };
                }else{
                    return new ApiResponseModel()
                    {
                        Status = false,
                        Error = "Failed to authenticate User!"
                    };
                }
            }
            //**END OF ZIKAIS CODE */
            return new ApiResponseModel()
            {
                Status = false
            };
        }

        // POST /api/users/expire OR :8080/users/expire
        [HttpPost]
        [Route("expire")]
        public ApiResponseModel Expire([FromBody]ExpireUserRequest expireRequest)
        {
            if (ModelState.IsValid)
            {
                // TODO: Find uuidv4 token belonging to an user and set it to null to expire it.
                ApplicationUser user = _dbContext.GetUserWithToken(expireRequest.Token);
                //TODO: SET TO NULL/
                if (user != null)
                {
                    return new ApiResponseModel()
                    {
                        Status = true
                    };
                }
            }

            return new ApiResponseModel()
            {
                Status = false
            };
        }

        // POST /api/users OR :8080/users
        [HttpPost]
        public ApiResponseModel Post([FromBody]RetrieveUserRequest retrieveRequest)
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
                        Age = new Random().Next(21, 30)
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
