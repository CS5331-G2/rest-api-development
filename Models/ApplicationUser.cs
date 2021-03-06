﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diary.Models;
using Microsoft.AspNetCore.Identity;

namespace diary.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UuidV4Token { get; set; }

        public string Fullname {get; set;}
        public int Age {get; set;}

        public List<Diary> Diaries { get; set; }
    }
}
