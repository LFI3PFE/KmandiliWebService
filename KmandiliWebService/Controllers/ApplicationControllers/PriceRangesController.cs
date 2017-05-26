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

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    [Authorize]
    public class PriceRangesController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/PriceRanges
        [AllowAnonymous]
        public IQueryable<PriceRange> GetPriceRanges()
        {
            return db.PriceRanges;
        }

        // GET: api/PriceRanges/5
        [ResponseType(typeof(PriceRange))]
        public IHttpActionResult GetPriceRange(int id)
        {
            PriceRange priceRange = db.PriceRanges.Find(id);
            if (priceRange == null)
            {
                return NotFound();
            }

            return Ok(priceRange);
        }

        // PUT: api/PriceRanges/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPriceRange(int id, PriceRange priceRange)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priceRange.ID)
            {
                return BadRequest();
            }

            db.Entry(priceRange).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceRangeExists(id))
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

        // POST: api/PriceRanges
        [ResponseType(typeof(PriceRange))]
        public IHttpActionResult PostPriceRange(PriceRange priceRange)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PriceRanges.Add(priceRange);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PriceRangeExists(priceRange.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = priceRange.ID }, priceRange);
        }

        // DELETE: api/PriceRanges/5
        [ResponseType(typeof(PriceRange))]
        public IHttpActionResult DeletePriceRange(int id)
        {
            PriceRange priceRange = db.PriceRanges.Find(id);
            if (priceRange == null)
            {
                return NotFound();
            }

            db.PriceRanges.Remove(priceRange);
            db.SaveChanges();

            return Ok(priceRange);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PriceRangeExists(int id)
        {
            return db.PriceRanges.Count(e => e.ID == id) > 0;
        }
    }
}