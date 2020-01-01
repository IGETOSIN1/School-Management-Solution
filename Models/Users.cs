using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WEB_SCONET_MANAGEMENT.Models
{
    public class Users
    {
        public string Full_Name { get; set; }
        public string Category_Status { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string confirmpassword { get; set; }
    }
}