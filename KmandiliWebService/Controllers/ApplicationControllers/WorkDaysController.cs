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
    public class WorkDaysController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/WorkDays
        public IQueryable<WorkDay> GetWorkDays()
        {
            return _db.WorkDays;
        }

        // GET: api/WorkDays/5
        [ResponseType(typeof(WorkDay))]
        public IHttpActionResult GetWorkDay(int id)
        {
            WorkDay workDay = _db.WorkDays.Find(id);
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

            _db.Entry(workDay).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
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

            _db.WorkDays.Add(workDay);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workDay.ID }, workDay);
        }

        // DELETE: api/WorkDays/5
        [ResponseType(typeof(WorkDay))]
        public IHttpActionResult DeleteWorkDay(int id)
        {
            WorkDay workDay = _db.WorkDays.Find(id);
            if (workDay == null)
            {
                return NotFound();
            }

            _db.WorkDays.Remove(workDay);
            _db.SaveChanges();

            return Ok(workDay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkDayExists(int id)
        {
            return _db.WorkDays.Count(e => e.ID == id) > 0;
        }
    }
}