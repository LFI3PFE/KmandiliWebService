﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KmandiliWebService.DatabaseAccessLayer;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    [Authorize]
    public class AddressesController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/Addresses
        public IQueryable<Address> GetAddresses()
        {
            return _db.Addresses;
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult GetAddress(int id)
        {
            Address address = _db.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.ID)
            {
                return BadRequest();
            }

            _db.Entry(address).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Addresses
        [ResponseType(typeof(Address))]
        [AllowAnonymous]
        public IHttpActionResult PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Addresses.Add(address);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = address.ID }, address);
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(int id)
        {
            Address address = _db.Addresses.Find(id);
            if (address == null)
            {
                return NotFound();
            }

            _db.Addresses.Remove(address);
            _db.SaveChanges();

            return Ok(address);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(int id)
        {
            return _db.Addresses.Count(e => e.ID == id) > 0;
        }
    }
}