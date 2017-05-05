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
    public class DeleveryDelaysController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/DeleveryDelays
        public IQueryable<DeleveryDelay> GetDeleveryDelays()
        {
            IQueryable<DeleveryDelay> q = db.DeleveryDelays;
            return q;
        }

        // GET: api/DeleveryDelays/5
        [ResponseType(typeof(DeleveryDelay))]
        public IHttpActionResult GetDeleveryDelay(int id)
        {
            DeleveryDelay deleveryDelay = db.DeleveryDelays.Find(id);
            if (deleveryDelay == null)
            {
                return NotFound();
            }

            return Ok(deleveryDelay);
        }

        // PUT: api/DeleveryDelays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeleveryDelay(int id, DeleveryDelay deleveryDelay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deleveryDelay.ID)
            {
                return BadRequest();
            }

            db.Entry(deleveryDelay).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeleveryDelayExists(id))
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

        // POST: api/DeleveryDelays
        [ResponseType(typeof(DeleveryDelay))]
        public IHttpActionResult PostDeleveryDelay(DeleveryDelay deleveryDelay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeleveryDelays.Add(deleveryDelay);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DeleveryDelayExists(deleveryDelay.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = deleveryDelay.ID }, deleveryDelay);
        }

        // DELETE: api/DeleveryDelays/5
        [ResponseType(typeof(DeleveryDelay))]
        public IHttpActionResult DeleteDeleveryDelay(int id)
        {
            DeleveryDelay deleveryDelay = db.DeleveryDelays.Find(id);
            if (deleveryDelay == null)
            {
                return NotFound();
            }

            db.DeleveryDelays.Remove(deleveryDelay);
            db.SaveChanges();

            return Ok(deleveryDelay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeleveryDelayExists(int id)
        {
            return db.DeleveryDelays.Count(e => e.ID == id) > 0;
        }
    }
}