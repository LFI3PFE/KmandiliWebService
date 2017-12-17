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
    public class PhoneNumbersController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/PhoneNumbers
        public IQueryable<PhoneNumber> GetPhoneNumbers()
        {
            return _db.PhoneNumbers;
        }

        // GET: api/PhoneNumbers/5
        [ResponseType(typeof(PhoneNumber))]
        public IHttpActionResult GetPhoneNumber(int id)
        {
            PhoneNumber phoneNumber = _db.PhoneNumbers.Find(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            return Ok(phoneNumber);
        }

        // PUT: api/PhoneNumbers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhoneNumber(int id, PhoneNumber phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phoneNumber.ID)
            {
                return BadRequest();
            }

            _db.Entry(phoneNumber).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PhoneNumbers
        [ResponseType(typeof(PhoneNumber))]
        [AllowAnonymous]
        public IHttpActionResult PostPhoneNumber(PhoneNumber phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (phoneNumber.User != null)
            {
                if (_db.Entry(phoneNumber.User).State == EntityState.Detached)
                {
                    _db.Users.Attach(phoneNumber.User);
                }
            }else if (phoneNumber.PastryShop != null)
            {
                if (_db.Entry(phoneNumber.PastryShop).State == EntityState.Detached)
                {
                    _db.PastryShops.Attach(phoneNumber.PastryShop);
                }
            }else if (phoneNumber.PointOfSale != null)
            {
                if (_db.Entry(phoneNumber.PointOfSale).State == EntityState.Detached)
                {
                    _db.PointOfSales.Attach(phoneNumber.PointOfSale);
                }
            }
            _db.PhoneNumbers.Add(phoneNumber);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = phoneNumber.ID }, phoneNumber);
        }

        // DELETE: api/PhoneNumbers/5
        [ResponseType(typeof(PhoneNumber))]
        public IHttpActionResult DeletePhoneNumber(int id)
        {
            PhoneNumber phoneNumber = _db.PhoneNumbers.Find(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            _db.PhoneNumbers.Remove(phoneNumber);
            _db.SaveChanges();

            return Ok(phoneNumber);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhoneNumberExists(int id)
        {
            return _db.PhoneNumbers.Count(e => e.ID == id) > 0;
        }
    }
}