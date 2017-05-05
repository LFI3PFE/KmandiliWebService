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
    public class PointOfSalesController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/PointOfSales
        public IQueryable<PointOfSale> GetPointOfSales()
        {
            return db.PointOfSales;
        }

        // GET: api/PointOfSales/5
        [ResponseType(typeof(PointOfSale))]
        public IHttpActionResult GetPointOfSale(int id)
        {
            PointOfSale pointOfSale = db.PointOfSales.Find(id);
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

            db.Entry(pointOfSale).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointOfSaleExists(id))
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

        // POST: api/PointOfSales
        [ResponseType(typeof(PointOfSale))]
        public IHttpActionResult PostPointOfSale(PointOfSale pointOfSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PointOfSales.Add(pointOfSale);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pointOfSale.ID }, pointOfSale);
        }

        // DELETE: api/PointOfSales/5
        [ResponseType(typeof(PointOfSale))]
        public IHttpActionResult DeletePointOfSale(int id)
        {
            PointOfSale pointOfSale = db.PointOfSales.Find(id);
            if (pointOfSale == null)
            {
                return NotFound();
            }

            db.PointOfSales.Remove(pointOfSale);
            db.SaveChanges();

            return Ok(pointOfSale);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PointOfSaleExists(int id)
        {
            return db.PointOfSales.Count(e => e.ID == id) > 0;
        }
    }
}