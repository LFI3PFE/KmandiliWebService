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
    public class ParkingsController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/Parkings
        public IQueryable<Parking> GetParkings()
        {
            return _db.Parkings;
        }

        // GET: api/Parkings/5
        [ResponseType(typeof(Parking))]
        public IHttpActionResult GetParking(int id)
        {
            Parking parking = _db.Parkings.Find(id);
            if (parking == null)
            {
                return NotFound();
            }

            return Ok(parking);
        }

        // PUT: api/Parkings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParking(int id, Parking parking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parking.ID)
            {
                return BadRequest();
            }

            _db.Entry(parking).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Parkings
        [ResponseType(typeof(Parking))]
        public IHttpActionResult PostParking(Parking parking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Parkings.Add(parking);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ParkingExists(parking.ID))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = parking.ID }, parking);
        }

        // DELETE: api/Parkings/5
        [ResponseType(typeof(Parking))]
        public IHttpActionResult DeleteParking(int id)
        {
            Parking parking = _db.Parkings.Find(id);
            if (parking == null)
            {
                return NotFound();
            }

            _db.Parkings.Remove(parking);
            _db.SaveChanges();

            return Ok(parking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParkingExists(int id)
        {
            return _db.Parkings.Count(e => e.ID == id) > 0;
        }
    }
}