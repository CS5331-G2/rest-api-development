using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.ApiModels.RootController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diary.Controllers
{
    [Route("api/[controller]")]
    public class RootController : Controller
    {
        private static readonly string[] IMPLEMENTED_ENDPOINTS = 
        {
            "/",
            "/meta/heartbeat",
            "/meta/members",
            "/users/register",
            "/users/authenticate",
            "/users/expire",
            "/users",
            "/diary",
            "/diary/create",
            "/diary/delete",
            "/diary/permission"
        };

        // GET /api/root OR GET :8080/
        [HttpGet]
        public ImplementedEndpointsResponse Get()
        {
            return new ImplementedEndpointsResponse()
            {
                Status = true,
                Result = IMPLEMENTED_ENDPOINTS.ToList()
            };
        }
    }
}
