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
    public class DiaryController : Controller
    {

        // GET /api/diary OR :8080/diary
        [HttpGet]
        public HeartbeatResponse Get()
        {
            return new HeartbeatResponse()
            {
                Status = true
            };
        }

        // POST /api/diary OR :8080/diary
        [HttpPost]
        public HeartbeatResponse Post()
        {
            return new HeartbeatResponse()
            {
                Status = true
            };
        }

        // POST /api/diary OR :8080/diary
        [HttpPost]
        [Route("create")]
        public HeartbeatResponse Create()
        {
            return new HeartbeatResponse()
            {
                Status = true
            };
        }

        // POST /api/diary OR :8080/diary
        [HttpPost]
        [Route("delete")]
        public HeartbeatResponse Delete()
        {
            return new HeartbeatResponse()
            {
                Status = true
            };
        }

        // POST /api/diary OR :8080/diary
        [HttpPost]
        [Route("permission")]
        public HeartbeatResponse Permission()
        {
            return new HeartbeatResponse()
            {
                Status = true
            };
        }
    }
}
