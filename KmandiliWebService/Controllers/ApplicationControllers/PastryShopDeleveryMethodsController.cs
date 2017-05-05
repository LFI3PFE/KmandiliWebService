using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KmandiliDataAccess;
using System.Configuration;
using System.Data.SqlClient;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    public class PastryShopDeleveryMethodsController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/PastryShopDeleveryMethods
        public IQueryable<PastryShopDeleveryMethod> GetPastryShopDeleveryMethods()
        {
            return db.PastryShopDeleveryMethods;
        }

        // GET: api/PastryShopDeleveryMethods/5
        [ResponseType(typeof(PastryShopDeleveryMethod))]
        public IHttpActionResult GetPastryShopDeleveryMethod(int id)
        {
            PastryShopDeleveryMethod pastryShopDeleveryMethod = db.PastryShopDeleveryMethods.Find(id);
            if (pastryShopDeleveryMethod == null)
            {
                return NotFound();
            }

            return Ok(pastryShopDeleveryMethod);
        }

        // PUT: api/PastryShopDeleveryMethods/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPastryShopDeleveryMethod(int id, PastryShopDeleveryMethod pastryShopDeleveryMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pastryShopDeleveryMethod.PastryShop_FK)
            {
                return BadRequest();
            }

            db.Entry(pastryShopDeleveryMethod).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastryShopDeleveryMethodExists(id))
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

        // POST: api/PastryShopDeleveryMethods
        [ResponseType(typeof(PastryShopDeleveryMethod))]
        public IHttpActionResult PostPastryShopDeleveryMethod(PastryShopDeleveryMethod pastryShopDeleveryMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.PastryShopDeleveryMethods.Add(pastryShopDeleveryMethod);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PastryShopDeleveryMethodExists(pastryShopDeleveryMethod.PastryShop_FK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = pastryShopDeleveryMethod.PastryShop_FK }, pastryShopDeleveryMethod);
        }

        // DELETE: api/PastryShopDeleveryMethods/5
        [ResponseType(typeof(PastryShopDeleveryMethod))]
        public IHttpActionResult DeletePastryShopDeleveryMethod(int id)
        {
            PastryShopDeleveryMethod pastryShopDeleveryMethod = db.PastryShopDeleveryMethods.Find(id);
            if (pastryShopDeleveryMethod == null)
            {
                return NotFound();
            }

            db.PastryShopDeleveryMethods.Remove(pastryShopDeleveryMethod);
            db.SaveChanges();

            return Ok(pastryShopDeleveryMethod);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PastryShopDeleveryMethodExists(int id)
        {
            return db.PastryShopDeleveryMethods.Count(e => e.PastryShop_FK == id) > 0;
        }
    }
}