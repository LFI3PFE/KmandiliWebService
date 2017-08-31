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
using KmandiliWebService.DatabaseAccessLayer;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    [Authorize]
    public class SaleUnitsController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/SaleUnits
        [AllowAnonymous]
        public IQueryable<SaleUnit> GetSaleUnits()
        {
            return db.SaleUnits;
        }

        // GET: api/SaleUnits/5
        [ResponseType(typeof(SaleUnit))]
        public IHttpActionResult GetSaleUnit(int id)
        {
            SaleUnit saleUnit = db.SaleUnits.Find(id);
            if (saleUnit == null)
            {
                return NotFound();
            }

            return Ok(saleUnit);
        }

        // PUT: api/SaleUnits/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSaleUnit(int id, SaleUnit saleUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != saleUnit.ID)
            {
                return BadRequest();
            }

            db.Entry(saleUnit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleUnitExists(id))
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

        // POST: api/SaleUnits
        [ResponseType(typeof(SaleUnit))]
        public IHttpActionResult PostSaleUnit(SaleUnit saleUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SaleUnits.Add(saleUnit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SaleUnitExists(saleUnit.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = saleUnit.ID }, saleUnit);
        }

        // DELETE: api/SaleUnits/5
        [ResponseType(typeof(SaleUnit))]
        public IHttpActionResult DeleteSaleUnit(int id)
        {
            SaleUnit saleUnit = db.SaleUnits.Find(id);
            if (saleUnit == null)
            {
                return NotFound();
            }

            db.SaleUnits.Remove(saleUnit);
            db.SaveChanges();

            return Ok(saleUnit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaleUnitExists(int id)
        {
            return db.SaleUnits.Count(e => e.ID == id) > 0;
        }
    }
}