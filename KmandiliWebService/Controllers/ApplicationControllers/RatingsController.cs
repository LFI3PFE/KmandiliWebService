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
    public class RatingsController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/Ratings
        public IQueryable<Rating> GetRatings()
        {
            return _db.Ratings;
        }

        // GET: api/Ratings/5
        [Route("api/Ratings/{user_fk}/{pastryShop_fk}/")]
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(int userFk, int pastryShopFk)
        {
            Rating rating = _db.Ratings.FirstOrDefault(r => r.User_FK == userFk && r.PastryShop_FK == pastryShopFk);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5
        [Route("api/Ratings/{user_fk}/{pastryShop_fk}/")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating(int userFk, int pastryShopFk, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userFk != rating.User_FK || pastryShopFk != rating.PastryShop_FK)
            {
                return BadRequest();
            }

            _db.Entry(rating).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(userFk, pastryShopFk))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Ratings.Add(rating);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RatingExists(rating.User_FK, rating.PastryShop_FK))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = rating.User_FK }, rating);
        }

        // DELETE: api/Ratings/5
        [Route("api/Ratings/{user_fk}/{pastryShop_fk}/")]
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int userFk, int pastryShopFk)
        {
            Rating rating = _db.Ratings.FirstOrDefault(r => r.User_FK == userFk && r.PastryShop_FK == pastryShopFk);
            if (rating == null)
            {
                return NotFound();
            }

            _db.Ratings.Remove(rating);
            _db.SaveChanges();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int userFk, int pastryShopFk)
        {
            return _db.Ratings.Count(e => e.User_FK == userFk && e.PastryShop_FK == pastryShopFk) > 0;
        }
    }
}