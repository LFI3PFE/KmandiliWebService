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
    public class OrderProductsController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        // GET: api/OrderProducts
        public IQueryable<OrderProduct> GetOrderProducts()
        {
            return db.OrderProducts;
        }

        // GET: api/OrderProducts/5
        [ResponseType(typeof(OrderProduct))]
        public IHttpActionResult GetOrderProduct(int id)
        {
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            return Ok(orderProduct);
        }

        // PUT: api/OrderProducts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderProduct(int id, OrderProduct orderProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderProduct.Order_FK)
            {
                return BadRequest();
            }

            db.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
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

        // POST: api/OrderProducts
        [ResponseType(typeof(OrderProduct))]
        public IHttpActionResult PostOrderProduct(OrderProduct orderProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderProducts.Add(orderProduct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderProductExists(orderProduct.Order_FK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderProduct.Order_FK }, orderProduct);
        }

        // DELETE: api/OrderProducts/5
        [ResponseType(typeof(OrderProduct))]
        public IHttpActionResult DeleteOrderProduct(int id)
        {
            OrderProduct orderProduct = db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            db.OrderProducts.Remove(orderProduct);
            db.SaveChanges();

            return Ok(orderProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderProductExists(int id)
        {
            return db.OrderProducts.Count(e => e.Order_FK == id) > 0;
        }
    }
}