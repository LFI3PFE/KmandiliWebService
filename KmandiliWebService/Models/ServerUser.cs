using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KmandiliWebService.Models
{
    public class ServerUser
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}