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
    public class PointOfSalesController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/PointOfSales
        public IQueryable<PointOfSale> GetPointOfSales()
        {
            return _db.PointOfSales;
        }

        // GET: api/PointOfSales/5
        [ResponseType(typeof(PointOfSale))]
        public IHttpActionResult GetPointOfSale(int id)
        {
            PointOfSale pointOfSale = _db.PointOfSales.Find(id);
            if (pointOfSale == null)
            {
                return NotFound();
            }

            return Ok(pointOfSale);
        }

        // PUT: api/PointOfSales/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPointOfSale(int id, PointOfSale pointOfSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pointOfSale.ID)
            {
                return BadRequest();
            }

            _db.Entry(pointOfSale).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointOfSaleExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PointOfSales
        [ResponseType(typeof(PointOfSale))]
        public IHttpActionResult PostPointOfSale(PointOfSale pointOfSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.PointOfSales.Add(pointOfSale);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pointOfSale.ID }, pointOfSale);
        }

        // DELETE: api/PointOfSales/5
        [ResponseType(typeof(PointOfSale))]
        public IHttpActionResult DeletePointOfSale(int id)
        {
            PointOfSale pointOfSale = _db.PointOfSales.Find(id);
            if (pointOfSale == null)
            {
                return NotFound();
            }
            var phoneNumbers = new List<PhoneNumber>(pointOfSale.PhoneNumbers);
            _db.PointOfSales.Remove(pointOfSale);
            phoneNumbers.ForEach(p => _db.PhoneNumbers.Remove(p));
            var a = _db.Addresses.Find(pointOfSale.Address_FK);
            if (a == null) return NotFound();
            _db.Addresses.Remove(a);
            _db.SaveChanges();
            return Ok(pointOfSale);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PointOfSaleExists(int id)
        {
            return _db.PointOfSales.Count(e => e.ID == id) > 0;
        }
    }
}