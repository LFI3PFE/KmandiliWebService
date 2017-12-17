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
    public class PhoneNumberTypesController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/PhoneNumberTypes
        [AllowAnonymous]
        public IQueryable<PhoneNumberType> GetPhoneNumberTypes()
        {
            return _db.PhoneNumberTypes;
        }

        // GET: api/PhoneNumberTypes/5
        [ResponseType(typeof(PhoneNumberType))]
        public IHttpActionResult GetPhoneNumberType(int id)
        {
            PhoneNumberType phoneNumberType = _db.PhoneNumberTypes.Find(id);
            if (phoneNumberType == null)
            {
                return NotFound();
            }

            return Ok(phoneNumberType);
        }

        // PUT: api/PhoneNumberTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhoneNumberType(int id, PhoneNumberType phoneNumberType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phoneNumberType.ID)
            {
                return BadRequest();
            }

            _db.Entry(phoneNumberType).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberTypeExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PhoneNumberTypes
        [ResponseType(typeof(PhoneNumberType))]
        public IHttpActionResult PostPhoneNumberType(PhoneNumberType phoneNumberType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.PhoneNumberTypes.Add(phoneNumberType);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PhoneNumberTypeExists(phoneNumberType.ID))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = phoneNumberType.ID }, phoneNumberType);
        }

        // DELETE: api/PhoneNumberTypes/5
        [ResponseType(typeof(PhoneNumberType))]
        public IHttpActionResult DeletePhoneNumberType(int id)
        {
            PhoneNumberType phoneNumberType = _db.PhoneNumberTypes.Find(id);
            if (phoneNumberType == null)
            {
                return NotFound();
            }

            _db.PhoneNumberTypes.Remove(phoneNumberType);
            _db.SaveChanges();

            return Ok(phoneNumberType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhoneNumberTypeExists(int id)
        {
            return _db.PhoneNumberTypes.Count(e => e.ID == id) > 0;
        }
    }
}