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
    public class PastryDeleveryPaymentsController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/PastryDeleveryPayments
        public IQueryable<PastryDeleveryPayment> GetPastryDeleveryPayments()
        {
            return db.PastryDeleveryPayments;
        }

        // GET: api/PastryDeleveryPayments/5
        [ResponseType(typeof(PastryDeleveryPayment))]
        public IHttpActionResult GetPastryDeleveryPayment(int id)
        {
            PastryDeleveryPayment pastryDeleveryPayment = db.PastryDeleveryPayments.Find(id);
            if (pastryDeleveryPayment == null)
            {
                return NotFound();
            }

            return Ok(pastryDeleveryPayment);
        }

        // PUT: api/PastryDeleveryPayments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPastryDeleveryPayment(int id, PastryDeleveryPayment pastryDeleveryPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pastryDeleveryPayment.ID)
            {
                return BadRequest();
            }

            db.Entry(pastryDeleveryPayment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastryDeleveryPaymentExists(id))
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

        // POST: api/PastryDeleveryPayments
        [ResponseType(typeof(PastryDeleveryPayment))]
        public IHttpActionResult PostPastryDeleveryPayment(PastryDeleveryPayment pastryDeleveryPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PastryDeleveryPayments.Add(pastryDeleveryPayment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PastryDeleveryPaymentExists(pastryDeleveryPayment.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pastryDeleveryPayment.ID }, pastryDeleveryPayment);
        }

        // DELETE: api/PastryDeleveryPayments/5
        [ResponseType(typeof(PastryDeleveryPayment))]
        public IHttpActionResult DeletePastryDeleveryPayment(int id)
        {
            PastryDeleveryPayment pastryDeleveryPayment = db.PastryDeleveryPayments.Find(id);
            if (pastryDeleveryPayment == null)
            {
                return NotFound();
            }

            db.PastryDeleveryPayments.Remove(pastryDeleveryPayment);
            db.SaveChanges();

            return Ok(pastryDeleveryPayment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PastryDeleveryPaymentExists(int id)
        {
            return db.PastryDeleveryPayments.Count(e => e.ID == id) > 0;
        }
    }
}