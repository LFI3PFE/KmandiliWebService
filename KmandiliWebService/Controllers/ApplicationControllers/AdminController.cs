using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using KmandiliDataAccess;
using KmandiliWebService.Models;
using Newtonsoft.Json;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    [Authorize]
    public class AdminController : ApiController
    {

        [Route("api/Admin")]
        [HttpGet]
        [ResponseType(typeof(Admin))]
        public IHttpActionResult GetAdmin()
        {
            Admin admin = new Admin()
            {
                UserName = System.Web.Configuration.WebConfigurationManager.AppSettings["AdminUserName"],
                Password = System.Web.Configuration.WebConfigurationManager.AppSettings["AdminPassword"],
            };
            return Ok(admin);
        }

        [Route("api/Admin/{userName}/{password}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateAdmin(string userName, string password)
        {
            try
            {
                var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                var section = (AppSettingsSection) config.GetSection("appSettings");
                section.Settings["AdminUserName"].Value = userName;
                section.Settings["AdminPassword"].Value = password;
                config.Save(ConfigurationSaveMode.Minimal);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [Route("api/GetEmailExist/{email}")]
        [HttpGet]
        [ResponseType(typeof (bool))]
        public bool GetEmailExist(string email)
        {
            var db = new KmandiliDBEntities();
            return (db.Users.Any(u => u.Email == email) || db.PastryShops.Any(p => p.Email == email) || System.Web.Configuration.WebConfigurationManager.AppSettings["AdminUserName"] == email);
        }
    }
}