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
    public class PastryShopDeleveryMethodsController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/PastryShopDeleveryMethods
        public IQueryable<PastryShopDeleveryMethod> GetPastryShopDeleveryMethods()
        {
            return _db.PastryShopDeleveryMethods;
        }

        // GET: api/PastryShopDeleveryMethods/5
        [ResponseType(typeof(PastryShopDeleveryMethod))]
        public IHttpActionResult GetPastryShopDeleveryMethod(int id)
        {
            PastryShopDeleveryMethod pastryShopDeleveryMethod = _db.PastryShopDeleveryMethods.Find(id);
            if (pastryShopDeleveryMethod == null)
            {
                return NotFound();
            }

            return Ok(pastryShopDeleveryMethod);
        }

        // PUT: api/PastryShopDeleveryMethods/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPastryShopDeleveryMethod(int id, PastryShopDeleveryMethod pastryShopDeleveryMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pastryShopDeleveryMethod.PastryShop_FK)
            {
                return BadRequest();
            }

            _db.Entry(pastryShopDeleveryMethod).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastryShopDeleveryMethodExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PastryShopDeleveryMethods
        [ResponseType(typeof(PastryShopDeleveryMethod))]
        [AllowAnonymous]
        public IHttpActionResult PostPastryShopDeleveryMethod(PastryShopDeleveryMethod pastryShopDeleveryMethod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.PastryShopDeleveryMethods.Add(pastryShopDeleveryMethod);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PastryShopDeleveryMethodExists(pastryShopDeleveryMethod.PastryShop_FK))
                {
                    return Conflict();
                }
                throw;
            }
            return CreatedAtRoute("DefaultApi", new { id = pastryShopDeleveryMethod.PastryShop_FK }, pastryShopDeleveryMethod);
        }

        // DELETE: api/PastryShopDeleveryMethods/5
        [ResponseType(typeof(PastryShopDeleveryMethod))]
        public IHttpActionResult DeletePastryShopDeleveryMethod(int id)
        {
            PastryShopDeleveryMethod pastryShopDeleveryMethod = _db.PastryShopDeleveryMethods.Find(id);
            if (pastryShopDeleveryMethod == null)
            {
                return NotFound();
            }

            _db.PastryShopDeleveryMethods.Remove(pastryShopDeleveryMethod);
            _db.SaveChanges();

            return Ok(pastryShopDeleveryMethod);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PastryShopDeleveryMethodExists(int id)
        {
            return _db.PastryShopDeleveryMethods.Count(e => e.PastryShop_FK == id) > 0;
        }
    }
}