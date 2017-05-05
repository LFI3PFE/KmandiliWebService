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
    public class PhoneNumberTypesController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/PhoneNumberTypes
        public IQueryable<PhoneNumberType> GetPhoneNumberTypes()
        {
            return db.PhoneNumberTypes;
        }

        // GET: api/PhoneNumberTypes/5
        [ResponseType(typeof(PhoneNumberType))]
        public IHttpActionResult GetPhoneNumberType(int id)
        {
            PhoneNumberType phoneNumberType = db.PhoneNumberTypes.Find(id);
            if (phoneNumberType == null)
            {
                return NotFound();
            }

            return Ok(phoneNumberType);
        }

        // PUT: api/PhoneNumberTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhoneNumberType(int id, PhoneNumberType phoneNumberType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phoneNumberType.ID)
            {
                return BadRequest();
            }

            db.Entry(phoneNumberType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberTypeExists(id))
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

        // POST: api/PhoneNumberTypes
        [ResponseType(typeof(PhoneNumberType))]
        public IHttpActionResult PostPhoneNumberType(PhoneNumberType phoneNumberType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhoneNumberTypes.Add(phoneNumberType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PhoneNumberTypeExists(phoneNumberType.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = phoneNumberType.ID }, phoneNumberType);
        }

        // DELETE: api/PhoneNumberTypes/5
        [ResponseType(typeof(PhoneNumberType))]
        public IHttpActionResult DeletePhoneNumberType(int id)
        {
            PhoneNumberType phoneNumberType = db.PhoneNumberTypes.Find(id);
            if (phoneNumberType == null)
            {
                return NotFound();
            }

            db.PhoneNumberTypes.Remove(phoneNumberType);
            db.SaveChanges();

            return Ok(phoneNumberType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhoneNumberTypeExists(int id)
        {
            return db.PhoneNumberTypes.Count(e => e.ID == id) > 0;
        }
    }
}