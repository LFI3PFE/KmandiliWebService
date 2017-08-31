using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using KmandiliWebService.DatabaseAccessLayer;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    [Authorize]
    public class PastryShopsController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/PastryShops
        public IQueryable<PastryShop> GetPastryShops()
        {
            return db.PastryShops;
        }

        // GET: api/PastryShops/5
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult GetPastryShop(int id)
        {
            PastryShop pastryShop = db.PastryShops.Find(id);
            if (pastryShop == null)
            {
                return NotFound();
            }

            return Ok(pastryShop);
        }

        [Route("api/PastryShops/{email}/{password}")]
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult GetPastryShop(string email, string password)
        {
            PastryShop pastryShop = db.PastryShops.FirstOrDefault(p => p.Email == email && p.Password == password);
            if (pastryShop == null)
            {
                return NotFound();
            }

            return Ok(pastryShop);
        }

        [Route("api/PastryShops/ByEmail/{email}/")]
        [AllowAnonymous]
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult GetPastryShop(string email)
        {
            PastryShop pastryShop = db.PastryShops.FirstOrDefault(p => p.Email == email);
            if (pastryShop == null)
            {
                return NotFound();
            }

            return Ok(pastryShop);
        }

        [Route("api/PastryShops/Categories/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPastryShopCategories(int id, PastryShop pastryShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pastryShop.ID)
            {
                return BadRequest();
            }

            var existingPastry = db.PastryShops.FirstOrDefault(p => p.ID == id);
            var toDeleteCategories = existingPastry.Categories.Except(pastryShop.Categories,c => c.ID).ToList<Category>();
            var toAddCategories = pastryShop.Categories.Except(existingPastry.Categories,c=> c.ID).ToList<Category>();

            toDeleteCategories.ForEach(c => existingPastry.Categories.Remove(c));
            foreach (var category in toAddCategories)
            {
                if (db.Entry(category).State == EntityState.Detached)
                    db.Categories.Attach(category);
                existingPastry.Categories.Add(category);
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastryShopExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/PastryShops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPastryShop(int id, PastryShop pastryShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pastryShop.ID)
            {
                return BadRequest();
            }
            
            db.Entry(pastryShop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastryShopExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PastryShops
        [ResponseType(typeof(PastryShop))]
        [AllowAnonymous]
        public IHttpActionResult PostPastryShop(PastryShop pastryShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (Category pastryShopCategory in pastryShop.Categories)
            {
                db.Entry(pastryShopCategory).State = EntityState.Unchanged;
            }
            db.PastryShops.Add(pastryShop);
            db.SaveChanges();

            //db.PastryShops.Add(pastryShop);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pastryShop.ID }, pastryShop);
        }

        // DELETE: api/PastryShops/5
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult DeletePastryShop(int id)
        {
            PastryShop pastryShop = db.PastryShops.Find(id);
            if (pastryShop == null)
            {
                return NotFound();
            }
            string profileFileName = pastryShop.ProfilePic.Substring(44, (pastryShop.ProfilePic.Length - 44));
            string profileImageFilePath = HostingEnvironment.MapPath("~/Uploads");
            profileImageFilePath = Path.Combine(profileImageFilePath, profileFileName);

            string coverFileName = pastryShop.CoverPic.Substring(44, (pastryShop.CoverPic.Length - 44));
            string coverImageFilePath = HostingEnvironment.MapPath("~/Uploads");
            coverImageFilePath = Path.Combine(coverImageFilePath, coverFileName);
            
            foreach (var product in pastryShop.Products)
            {
                string productFileName = product.Pic.Substring(44, (product.Pic.Length - 44));
                string productImageFilePath = HostingEnvironment.MapPath("~/Uploads");
                productImageFilePath = Path.Combine(productImageFilePath, productFileName);
                File.Delete(productImageFilePath);
            }

            var poinOfSalesPhoneNumbers = new List<PhoneNumber>();
            foreach (var pointOfSale in pastryShop.PointOfSales)
            {
               poinOfSalesPhoneNumbers.AddRange(pointOfSale.PhoneNumbers);
            }

            var phoneNumbers = new List<PhoneNumber>(pastryShop.PhoneNumbers);
            var orders = new List<Order>(pastryShop.Orders);
            db.PastryShops.Remove(pastryShop);
            phoneNumbers.ForEach(p => db.PhoneNumbers.Remove(p));
            poinOfSalesPhoneNumbers.ForEach(p => db.PhoneNumbers.Remove(p));
            orders.ForEach(o => db.Orders.Remove(o));
            db.Addresses.Remove(db.Addresses.Find(pastryShop.Address_FK));
            
            db.SaveChanges();
            File.Delete(profileImageFilePath);
            File.Delete(coverImageFilePath);
            return Ok(pastryShop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PastryShopExists(int id)
        {
            return db.PastryShops.Count(e => e.ID == id) > 0;
        }
        
    }
}