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
    public class DeleveryMethodsController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/DeleveryMethods
        public IQueryable<DeleveryMethod> GetDeleveryMethods()
        {
            return db.DeleveryMethods;
        }

        // GET: api/DeleveryMethods/5
        [ResponseType(typeof(DeleveryMethod))]
        public IHttpActionResult GetDeleveryMethod(int id)
        {
            DeleveryMethod deleveryMethod = db.DeleveryMethods.Find(id);
            if (deleveryMethod == null)
            {
                return NotFound();
            }

            return Ok(deleveryMethod);
        }

        // PUT: api/DeleveryMethods/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeleveryMethod(int id, DeleveryMethod deleveryMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deleveryMethod.ID)
            {
                return BadRequest();
            }

            db.Entry(deleveryMethod).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeleveryMethodExists(id))
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

        // POST: api/DeleveryMethods
        [ResponseType(typeof(DeleveryMethod))]
        public IHttpActionResult PostDeleveryMethod(DeleveryMethod deleveryMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeleveryMethods.Add(deleveryMethod);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DeleveryMethodExists(deleveryMethod.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = deleveryMethod.ID }, deleveryMethod);
        }

        // DELETE: api/DeleveryMethods/5
        [ResponseType(typeof(DeleveryMethod))]
        public IHttpActionResult DeleteDeleveryMethod(int id)
        {
            DeleveryMethod deleveryMethod = db.DeleveryMethods.Find(id);
            if (deleveryMethod == null)
            {
                return NotFound();
            }

            db.DeleveryMethods.Remove(deleveryMethod);
            db.SaveChanges();

            return Ok(deleveryMethod);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeleveryMethodExists(int id)
        {
            return db.DeleveryMethods.Count(e => e.ID == id) > 0;
        }
    }
}