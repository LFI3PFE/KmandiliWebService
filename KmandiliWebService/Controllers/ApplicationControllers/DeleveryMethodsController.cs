using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KmandiliWebService.DatabaseAccessLayer;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    [Authorize]
    public class DeleveryMethodsController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/DeleveryMethods
        [AllowAnonymous]
        public IQueryable<DeleveryMethod> GetDeleveryMethods()
        {
            return _db.DeleveryMethods;
        }

        // GET: api/DeleveryMethods/5
        [ResponseType(typeof(DeleveryMethod))]
        public IHttpActionResult GetDeleveryMethod(int id)
        {
            DeleveryMethod deleveryMethod = _db.DeleveryMethods.Find(id);
            if (deleveryMethod == null)
            {
                return NotFound();
            }

            return Ok(deleveryMethod);
        }

        [Route("api/DeleveryMethods/Payments/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDeleveryMethodPayments(int id, DeleveryMethod deleveryMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deleveryMethod.ID)
            {
                return BadRequest();
            }

            var existingDeleveryMethod = _db.DeleveryMethods.FirstOrDefault(d => d.ID == id);
            if (existingDeleveryMethod == null) return NotFound();
            var toDeletePayments = existingDeleveryMethod.Payments.Except(deleveryMethod.Payments, p => p.ID).ToList();
            var toAddPayments = deleveryMethod.Payments.Except(existingDeleveryMethod.Payments, p => p.ID).ToList();

            toDeletePayments.ForEach(c => existingDeleveryMethod.Payments.Remove(c));
            foreach (var payment in toAddPayments)
            {
                if (_db.Entry(payment).State == EntityState.Detached)
                    _db.Payments.Attach(payment);
                existingDeleveryMethod.Payments.Add(payment);
            }

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeleveryMethodExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
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

            _db.Entry(deleveryMethod).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeleveryMethodExists(id))
                {
                    return NotFound();
                }
                throw;
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

            var toAddPayments = new List<Payment>(deleveryMethod.Payments);
            deleveryMethod.Payments.Clear();

            _db.DeleveryMethods.Add(deleveryMethod);

            foreach (var payment in toAddPayments)
            {
                if (_db.Entry(payment).State == EntityState.Detached)
                    _db.Payments.Attach(payment);
                deleveryMethod.Payments.Add(payment);
            }

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DeleveryMethodExists(deleveryMethod.ID))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = deleveryMethod.ID }, deleveryMethod);
        }

        // DELETE: api/DeleveryMethods/5
        [ResponseType(typeof(DeleveryMethod))]
        public IHttpActionResult DeleteDeleveryMethod(int id)
        {
            DeleveryMethod deleveryMethod = _db.DeleveryMethods.Find(id);
            if (deleveryMethod == null)
            {
                return NotFound();
            }

            _db.DeleveryMethods.Remove(deleveryMethod);
            _db.SaveChanges();

            return Ok(deleveryMethod);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeleveryMethodExists(int id)
        {
            return _db.DeleveryMethods.Count(e => e.ID == id) > 0;
        }
    }
}