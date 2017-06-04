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
    [Authorize]
    public class RatingsController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/Ratings
        public IQueryable<Rating> GetRatings()
        {
            return db.Ratings;
        }

        // GET: api/Ratings/5
        [Route("api/Ratings/{user_fk}/{pastryShop_fk}/")]
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(int user_fk, int pastryShop_fk)
        {
            Rating rating = db.Ratings.FirstOrDefault(r => r.User_FK == user_fk && r.PastryShop_FK == pastryShop_fk);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5
        [Route("api/Ratings/{user_fk}/{pastryShop_fk}/")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating(int user_fk, int pastryShop_fk, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user_fk != rating.User_FK || pastryShop_fk != rating.PastryShop_FK)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(user_fk, pastryShop_fk))
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

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ratings.Add(rating);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RatingExists(rating.User_FK, rating.PastryShop_FK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rating.User_FK }, rating);
        }

        // DELETE: api/Ratings/5
        [Route("api/Ratings/{user_fk}/{pastryShop_fk}/")]
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int user_fk, int pastryShop_fk)
        {
            Rating rating = db.Ratings.FirstOrDefault(r => r.User_FK == user_fk && r.PastryShop_FK == pastryShop_fk);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            db.SaveChanges();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int user_fk, int pastryShop_fk)
        {
            return db.Ratings.Count(e => e.User_FK == user_fk && e.PastryShop_FK == pastryShop_fk) > 0;
        }
    }
}