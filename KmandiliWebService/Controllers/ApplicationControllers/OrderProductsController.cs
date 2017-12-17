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
    public class OrderProductsController : ApiController
    {
        private readonly KmandiliDBEntities _db = new KmandiliDBEntities();

        // GET: api/OrderProducts
        public IQueryable<OrderProduct> GetOrderProducts()
        {
            return _db.OrderProducts;
        }

        // GET: api/OrderProducts/5
        [ResponseType(typeof(OrderProduct))]
        public IHttpActionResult GetOrderProduct(int id)
        {
            OrderProduct orderProduct = _db.OrderProducts.Find(id);
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

            _db.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductExists(id))
                {
                    return NotFound();
                }
                throw;
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

            _db.OrderProducts.Add(orderProduct);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderProductExists(orderProduct.Order_FK))
                {
                    return Conflict();
                }
                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = orderProduct.Order_FK }, orderProduct);
        }

        // DELETE: api/OrderProducts/5
        [ResponseType(typeof(OrderProduct))]
        public IHttpActionResult DeleteOrderProduct(int id)
        {
            OrderProduct orderProduct = _db.OrderProducts.Find(id);
            if (orderProduct == null)
            {
                return NotFound();
            }

            _db.OrderProducts.Remove(orderProduct);
            _db.SaveChanges();

            return Ok(orderProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderProductExists(int id)
        {
            return _db.OrderProducts.Count(e => e.Order_FK == id) > 0;
        }
    }
}