using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KmandiliDataAccess;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    public class PasswordController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

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
                var user = db.Users.FirstOrDefault(u => u.Email == email);
                if (user == null)
                {
                    var pastryShop = db.PastryShops.FirstOrDefault(u => u.Email == email);
                    if (pastryShop != null)
                    {
                        pastryShop.Password = newPassword;
                        db.Entry(pastryShop).State = EntityState.Modified;
                    }
                }
                else
                {
                    user.Password = newPassword;
                    db.Entry(user).State = EntityState.Modified;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
