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
    public class DeleveryDelaysController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/DeleveryDelays
        [AllowAnonymous]
        public IQueryable<DeleveryDelay> GetDeleveryDelays()
        {
            IQueryable<DeleveryDelay> q = _db.DeleveryDelays;
            return q;
        }

        // GET: api/DeleveryDelays/5
        [ResponseType(typeof(DeleveryDelay))]
        public IHttpActionResult GetDeleveryDelay(int id)
        {
            DeleveryDelay deleveryDelay = _db.DeleveryDelays.Find(id);
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

            _db.Entry(deleveryDelay).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeleveryDelayExists(id))
                {
                    return NotFound();
                }
                throw;
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

            _db.DeleveryDelays.Add(deleveryDelay);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DeleveryDelayExists(deleveryDelay.ID))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = deleveryDelay.ID }, deleveryDelay);
        }

        // DELETE: api/DeleveryDelays/5
        [ResponseType(typeof(DeleveryDelay))]
        public IHttpActionResult DeleteDeleveryDelay(int id)
        {
            DeleveryDelay deleveryDelay = _db.DeleveryDelays.Find(id);
            if (deleveryDelay == null)
            {
                return NotFound();
            }

            _db.DeleveryDelays.Remove(deleveryDelay);
            _db.SaveChanges();

            return Ok(deleveryDelay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeleveryDelayExists(int id)
        {
            return _db.DeleveryDelays.Count(e => e.ID == id) > 0;
        }
    }
}