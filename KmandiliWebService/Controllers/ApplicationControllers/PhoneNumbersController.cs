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
    public class PhoneNumbersController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/PhoneNumbers
        public IQueryable<PhoneNumber> GetPhoneNumbers()
        {
            return db.PhoneNumbers;
        }

        // GET: api/PhoneNumbers/5
        [ResponseType(typeof(PhoneNumber))]
        public IHttpActionResult GetPhoneNumber(int id)
        {
            PhoneNumber phoneNumber = db.PhoneNumbers.Find(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            return Ok(phoneNumber);
        }

        // PUT: api/PhoneNumbers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhoneNumber(int id, PhoneNumber phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phoneNumber.ID)
            {
                return BadRequest();
            }

            db.Entry(phoneNumber).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberExists(id))
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

        // POST: api/PhoneNumbers
        [ResponseType(typeof(PhoneNumber))]
        public IHttpActionResult PostPhoneNumber(PhoneNumber phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (phoneNumber.User != null)
            {
                if (db.Entry(phoneNumber.User).State == EntityState.Detached)
                {
                    db.Users.Attach(phoneNumber.User);
                }
            }else if (phoneNumber.PastryShop != null)
            {
                if (db.Entry(phoneNumber.PastryShop).State == EntityState.Detached)
                {
                    db.PastryShops.Attach(phoneNumber.PastryShop);
                }
            }else if (phoneNumber.PointOfSale != null)
            {
                if (db.Entry(phoneNumber.PointOfSale).State == EntityState.Detached)
                {
                    db.PointOfSales.Attach(phoneNumber.PointOfSale);
                }
            }
            db.PhoneNumbers.Add(phoneNumber);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = phoneNumber.ID }, phoneNumber);
        }

        // DELETE: api/PhoneNumbers/5
        [ResponseType(typeof(PhoneNumber))]
        public IHttpActionResult DeletePhoneNumber(int id)
        {
            PhoneNumber phoneNumber = db.PhoneNumbers.Find(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            db.PhoneNumbers.Remove(phoneNumber);
            db.SaveChanges();

            return Ok(phoneNumber);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhoneNumberExists(int id)
        {
            return db.PhoneNumbers.Count(e => e.ID == id) > 0;
        }
    }
}