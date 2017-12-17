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
    public class PastryDeleveryPaymentsController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/PastryDeleveryPayments
        public IQueryable<PastryDeleveryPayment> GetPastryDeleveryPayments()
        {
            return _db.PastryDeleveryPayments;
        }

        // GET: api/PastryDeleveryPayments/5
        [ResponseType(typeof(PastryDeleveryPayment))]
        public IHttpActionResult GetPastryDeleveryPayment(int id)
        {
            PastryDeleveryPayment pastryDeleveryPayment = _db.PastryDeleveryPayments.Find(id);
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

            _db.Entry(pastryDeleveryPayment).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastryDeleveryPaymentExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PastryDeleveryPayments
        [ResponseType(typeof(PastryDeleveryPayment))]
        [AllowAnonymous]
        public IHttpActionResult PostPastryDeleveryPayment(PastryDeleveryPayment pastryDeleveryPayment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.PastryDeleveryPayments.Add(pastryDeleveryPayment);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PastryDeleveryPaymentExists(pastryDeleveryPayment.ID))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = pastryDeleveryPayment.ID }, pastryDeleveryPayment);
        }

        // DELETE: api/PastryDeleveryPayments/5
        [ResponseType(typeof(PastryDeleveryPayment))]
        public IHttpActionResult DeletePastryDeleveryPayment(int id)
        {
            PastryDeleveryPayment pastryDeleveryPayment = _db.PastryDeleveryPayments.Find(id);
            if (pastryDeleveryPayment == null)
            {
                return NotFound();
            }

            _db.PastryDeleveryPayments.Remove(pastryDeleveryPayment);
            _db.SaveChanges();

            return Ok(pastryDeleveryPayment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PastryDeleveryPaymentExists(int id)
        {
            return _db.PastryDeleveryPayments.Count(e => e.ID == id) > 0;
        }
    }
}