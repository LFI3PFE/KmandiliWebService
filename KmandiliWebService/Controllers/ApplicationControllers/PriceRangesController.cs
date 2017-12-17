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
    public class PriceRangesController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/PriceRanges
        [AllowAnonymous]
        public IQueryable<PriceRange> GetPriceRanges()
        {
            return _db.PriceRanges;
        }

        // GET: api/PriceRanges/5
        [ResponseType(typeof(PriceRange))]
        public IHttpActionResult GetPriceRange(int id)
        {
            PriceRange priceRange = _db.PriceRanges.Find(id);
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

            _db.Entry(priceRange).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceRangeExists(id))
                {
                    return NotFound();
                }
                throw;
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

            _db.PriceRanges.Add(priceRange);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PriceRangeExists(priceRange.ID))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = priceRange.ID }, priceRange);
        }

        // DELETE: api/PriceRanges/5
        [ResponseType(typeof(PriceRange))]
        public IHttpActionResult DeletePriceRange(int id)
        {
            PriceRange priceRange = _db.PriceRanges.Find(id);
            if (priceRange == null)
            {
                return NotFound();
            }

            _db.PriceRanges.Remove(priceRange);
            _db.SaveChanges();

            return Ok(priceRange);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PriceRangeExists(int id)
        {
            return _db.PriceRanges.Count(e => e.ID == id) > 0;
        }
    }
}