using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.ApiModels.MetaController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diary.Controllers
{
    [Route("api/[controller]")]
    public class MetaController : Controller
    {
        private static readonly string[] MEMBERS = 
        {
            "Muhammad Mustaqiim Bin Muhar",
            "Ng Qing Hua",
            "Ng Zi Kai",
            "Yee Jian Feng, Eric"
        };


        // GET /api/meta/heartbeat OR :8080/meta/heartbeat
        [HttpGet]
        [Route("heartbeat")]
        public HeartbeatResponse Heartbeat()
        {
            return new HeartbeatResponse()
            {
                Status = true
            };
        }
        
        // GET /api/meta/members OR :8080/meta/members
        [HttpGet]
        [Route("members")]
        public MembersResponse Members()
        {
            return new MembersResponse()
            {
                Status = true,
                Result = MEMBERS.ToList()
            };
        }
    }
}
