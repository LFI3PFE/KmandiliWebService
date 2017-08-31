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
    public class WorkDaysController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/WorkDays
        public IQueryable<WorkDay> GetWorkDays()
        {
            return db.WorkDays;
        }

        // GET: api/WorkDays/5
        [ResponseType(typeof(WorkDay))]
        public IHttpActionResult GetWorkDay(int id)
        {
            WorkDay workDay = db.WorkDays.Find(id);
            if (workDay == null)
            {
                return NotFound();
            }

            return Ok(workDay);
        }

        // PUT: api/WorkDays/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkDay(int id, WorkDay workDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workDay.ID)
            {
                return BadRequest();
            }

            db.Entry(workDay).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkDayExists(id))
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

        // POST: api/WorkDays
        [ResponseType(typeof(WorkDay))]
        public IHttpActionResult PostWorkDay(WorkDay workDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkDays.Add(workDay);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workDay.ID }, workDay);
        }

        // DELETE: api/WorkDays/5
        [ResponseType(typeof(WorkDay))]
        public IHttpActionResult DeleteWorkDay(int id)
        {
            WorkDay workDay = db.WorkDays.Find(id);
            if (workDay == null)
            {
                return NotFound();
            }

            db.WorkDays.Remove(workDay);
            db.SaveChanges();

            return Ok(workDay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkDayExists(int id)
        {
            return db.WorkDays.Count(e => e.ID == id) > 0;
        }
    }
}