﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KmandiliDataAccess;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    public class PastryShopsController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/PastryShops
        public IQueryable<PastryShop> GetPastryShops()
        {
            return db.PastryShops;
        }

        // GET: api/PastryShops/5
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult GetPastryShop(int id)
        {
            PastryShop pastryShop = db.PastryShops.Find(id);
            if (pastryShop == null)
            {
                return NotFound();
            }

            return Ok(pastryShop);
        }

        [Route("api/PastryShops/{email}/{password}")]
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult GetPastryShop(string email, string password)
        {
            PastryShop pastryShop = db.PastryShops.FirstOrDefault(p => p.Email == email && p.Password == password);
            if (pastryShop == null)
            {
                return NotFound();
            }

            return Ok(pastryShop);
        }

        [Route("api/PastryShops/ByEmail/{email}/")]
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult GetPastryShop(string email)
        {
            PastryShop pastryShop = db.PastryShops.FirstOrDefault(p => p.Email == email);
            if (pastryShop == null)
            {
                return NotFound();
            }

            return Ok(pastryShop);
        }

        // PUT: api/PastryShops/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPastryShop(int id, PastryShop pastryShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pastryShop.ID)
            {
                return BadRequest();
            }

            db.Entry(pastryShop).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastryShopExists(id))
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

        // POST: api/PastryShops
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult PostPastryShop(PastryShop pastryShop)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (Category pastryShopCategory in pastryShop.Categories)
            {
                db.Entry(pastryShopCategory).State = EntityState.Unchanged;
            }
            db.PastryShops.Add(pastryShop);
            db.SaveChanges();

            //db.PastryShops.Add(pastryShop);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pastryShop.ID }, pastryShop);
        }

        // DELETE: api/PastryShops/5
        [ResponseType(typeof(PastryShop))]
        public IHttpActionResult DeletePastryShop(int id)
        {
            PastryShop pastryShop = db.PastryShops.Find(id);
            if (pastryShop == null)
            {
                return NotFound();
            }

            db.PastryShops.Remove(pastryShop);
            db.SaveChanges();

            return Ok(pastryShop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PastryShopExists(int id)
        {
            return db.PastryShops.Count(e => e.ID == id) > 0;
        }
    }
}