using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using KmandiliWebService.DatabaseAccessLayer;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    [Authorize]
    public class PastryShopsController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/PastryShops
        public IQueryable<PastryShop> GetPastryShops()
        {
            return _db.PastryShops;
        }

        // GET: api/PastryShops/5
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult GetPastryShop(int id)
        {
            PastryShop pastryShop = _db.PastryShops.Find(id);
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
            PastryShop pastryShop = _db.PastryShops.FirstOrDefault(p => p.Email == email && p.Password == password);
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
            PastryShop pastryShop = _db.PastryShops.FirstOrDefault(p => p.Email == email);
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

            var existingPastry = _db.PastryShops.FirstOrDefault(p => p.ID == id);
            if (existingPastry == null) return NotFound();
            var toDeleteCategories = existingPastry.Categories.Except(pastryShop.Categories,c => c.ID).ToList();
            var toAddCategories = pastryShop.Categories.Except(existingPastry.Categories,c=> c.ID).ToList();

            toDeleteCategories.ForEach(c => existingPastry.Categories.Remove(c));
            foreach (var category in toAddCategories)
            {
                if (_db.Entry(category).State == EntityState.Detached)
                    _db.Categories.Attach(category);
                existingPastry.Categories.Add(category);
            }

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastryShopExists(id))
                {
                    return NotFound();
                }
                throw;
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
            
            _db.Entry(pastryShop).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastryShopExists(id))
                {
                    return NotFound();
                }
                throw;
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
                _db.Entry(pastryShopCategory).State = EntityState.Unchanged;
            }
            _db.PastryShops.Add(pastryShop);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pastryShop.ID }, pastryShop);
        }

        // DELETE: api/PastryShops/5
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult DeletePastryShop(int id)
        {
            PastryShop pastryShop = _db.PastryShops.Find(id);
            if (pastryShop == null)
            {
                return NotFound();
            }
            string profileFileName = pastryShop.ProfilePic.Substring(44, (pastryShop.ProfilePic.Length - 44));
            string profileImageFilePath = HostingEnvironment.MapPath("~/Uploads");
            if (profileImageFilePath == null) return NotFound();
            profileImageFilePath = Path.Combine(profileImageFilePath, profileFileName);

            string coverFileName = pastryShop.CoverPic.Substring(44, (pastryShop.CoverPic.Length - 44));
            string coverImageFilePath = HostingEnvironment.MapPath("~/Uploads");
            if (coverImageFilePath == null) return NotFound();
            coverImageFilePath = Path.Combine(coverImageFilePath, coverFileName);
            
            foreach (var product in pastryShop.Products)
            {
                string productFileName = product.Pic.Substring(44, (product.Pic.Length - 44));
                string productImageFilePath = HostingEnvironment.MapPath("~/Uploads");
                if (productImageFilePath == null) return NotFound();
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
            _db.PastryShops.Remove(pastryShop);
            phoneNumbers.ForEach(p => _db.PhoneNumbers.Remove(p));
            poinOfSalesPhoneNumbers.ForEach(p => _db.PhoneNumbers.Remove(p));
            orders.ForEach(o => _db.Orders.Remove(o));
            var a = _db.Addresses.Find(pastryShop.Address_FK);
            if (a == null) return NotFound();
            _db.Addresses.Remove(a);
            
            _db.SaveChanges();
            File.Delete(profileImageFilePath);
            File.Delete(coverImageFilePath);
            return Ok(pastryShop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PastryShopExists(int id)
        {
            return _db.PastryShops.Count(e => e.ID == id) > 0;
        }
        
    }
}