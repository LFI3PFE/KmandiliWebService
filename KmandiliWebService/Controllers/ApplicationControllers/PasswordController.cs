﻿using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KmandiliWebService.DatabaseAccessLayer;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    public class PasswordController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        [Route("api/passwords/{email}/{newPassword}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPassword(string email, string newPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (email == System.Web.Configuration.WebConfigurationManager.AppSettings["AdminUserName"])
            {
                var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                var section = (AppSettingsSection)config.GetSection("appSettings");
                section.Settings["AdminPassword"].Value = newPassword;
                config.Save(ConfigurationSaveMode.Minimal);
            }
            else
            {
                var user = _db.Users.FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    var pastryShop = _db.PastryShops.FirstOrDefault(u => u.Email == email);
                    if (pastryShop != null)
                    {
                        pastryShop.Password = newPassword;
                        _db.Entry(pastryShop).State = EntityState.Modified;
                    }
                }
                else
                {
                    user.Password = newPassword;
                    _db.Entry(user).State = EntityState.Modified;
                }

                _db.SaveChanges();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
