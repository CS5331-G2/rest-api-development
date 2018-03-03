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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
namespace diary.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(ApplicationDbContext dbContext, 
                               UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // POST /api/users/register OR :8080/users/register
        [HttpPost]
        [Route("register")]
        public async Task<ApiResponseModel> Register([FromBody]RegisterUserRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
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
                    String passwordHash = System.Text.Encoding.Unicode.GetString(hashbytes);

                    ApplicationUser newUser = new ApplicationUser()
                    {
                        UuidV4Token = System.Guid.NewGuid().ToString(),
                        UserName = registerRequest.Username,
                        Fullname = registerRequest.FullName,
                        Age = registerRequest.Age,
                    };

                    var created = await _userManager.CreateAsync(newUser, registerRequest.Password);

                    if(created.Succeeded){
                        return new ApiResponseModel(){
                            Status = true
                        };
                    }else{
                        return new ApiResponseModel(){
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
        }

        // POST /api/users/authenticate OR :8080/users/authenticate
        [HttpPost]
        [Route("authenticate")]
        public async Task<ApiResponseModel> Authenticate([FromBody]AuthenticateUserRequest authenticationRequest)
        {
            if (ModelState.IsValid)
            {
                // TODO: perform authentication
                SHA256 newSHA = SHA256Managed.Create();
                string username = authenticationRequest.Username;
                string password = System.Text.Encoding.Unicode.GetString(
                    newSHA.ComputeHash(System.Text.Encoding.Unicode.GetBytes(
                        authenticationRequest.Password)));

                var result =  await _signInManager.PasswordSignInAsync(username, password,false, false);
                if (result.Succeeded)
                {
                    //Assign token to user
                    String token = Guid.NewGuid().ToString();

                    ApplicationUser user = _dbContext.Users.Find(username);
                    user.UuidV4Token  = token;
                    _dbContext.Users.Update(user);
                    if(_dbContext.SaveChanges() > 0){
                        return new ApiResponseModel()
                        {
                            Status = true,
                            Result = new AuthenticateUserResultModel()
                            {
                                Token = token
                            }
                        };
                    }else{
                        return new ApiResponseModel()
                        {
                            Status = false,
                            Error = "Database Error!"
                        };
                    }
                    
                }else{
                    return new ApiResponseModel()
                    {
                        Status = false,
                        Error = "Failed to authenticate User!"
                    };
                }
            }
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
                if (user != null)
                {
                    user.UuidV4Token = null;
                    _dbContext.Users.Update(user);
                    if(_dbContext.SaveChanges() > 0){
                        return new ApiResponseModel()
                        {
                            Status = true
                        };
                    }else{
                        return new ApiResponseModel()
                        {
                            Status =  false,
                            Error = "Database Error!"
                        };
                    }

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
                ApplicationUser  user = _dbContext.GetUserWithToken(retrieveRequest.Token);

                if(user != null){
                    return new ApiResponseModel()
                    {
                        Status = true,
                        Result = new RetrieveUserResultModel()
                        {
                            Username = user.UserName,
                            Fullname = user.Fullname,
                            Age = user.Age
                        }
                    };
                }else{
                    return new ApiResponseModel()
                    {
                        Status = false,
                        Error = "Failed to retrieve user!"
                    };
                }
            }

            return new ApiResponseModel()
            {
                Status = false,
                Error = "Invalid authentication token."
            };
        }
    }
}
