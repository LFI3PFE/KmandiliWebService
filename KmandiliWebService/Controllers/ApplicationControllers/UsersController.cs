﻿using System;
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
using WebGrease.Css.Extensions;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    public class UsersController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("api/Users/{email}/{password}")]
        public IHttpActionResult GetUser(string email, string password)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        [Route("api/Users/ByEmail/{email}/")]
        public IHttpActionResult GetUser(string email)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var phoneNumbers = new List<PhoneNumber>(user.PhoneNumbers);
            db.Users.Remove(user);
            phoneNumbers.ForEach(p => db.PhoneNumbers.Remove(p));
            db.Addresses.Remove(db.Addresses.Find(user.Address_FK));
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}