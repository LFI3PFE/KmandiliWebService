using KmandiliDataAccess;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.WebPages;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    public class EmailsController : ApiController
    {
        private KmandiliDBEntities db = new KmandiliDBEntities();

        const string ProductCellTemplate = "<tr>" +
                            "<td colspan=\"2\" class=\"desc\" style=\"padding: 20px;background: #EEEEEE;text-align: left;border-bottom: 1px solid #FFFFFF;\">" +
                                "<div style=\"display: table;\">" +
                                    "<div class=\"ProdImgDiv\" style=\"align-self: center;display: table-cell; float: none; vertical-align: middle;\">" +
                                        "<img src=\"#ImgSRC\" style =\"width: 130px;margin-right: 30%;\">" +
                                    "</div>" +
                                    "<div class=\"ProdInfoDiv\" style=\"align-self: center;display: table-cell; float: none; vertical-align: middle;\">" +
                                        "<h3 id=\"Prod\" style=\"color: #57B223;font-size: 1.2em;font-weight: normal;margin: 0 0 0.2em 0;\">#ProductName</h3>" +
                                        "#ProductDescription" +
                                    "</div>" +
                                "</div>" +
                            "</td>" +
                            "<td class=\"unit\" style=\"padding: 20px;background: #DDDDDD;text-align: right;border-bottom: 1px solid #FFFFFF;font-size: 1.2em;\">#ProductPrice</td>" +
                            "<td class=\"qty\" style=\"padding: 20px;background: #EEEEEE;text-align: right;border-bottom: 1px solid #FFFFFF;font-size: 1.2em;\">#ProductQuantity</td>" +
                            "<td class=\"total\" style=\"padding: 20px;background: #57B223;text-align: right;border-bottom: 1px solid #FFFFFF;color: #FFFFFF;font-size: 1.2em;\">#ProductTotal</td>" +
                        "</tr>";
        private const string NoticeString = "<div class=\"BodyHeader\" style=\"display: flex; padding-bottom: 2%;\">" +
                    "<div class=\"PastryInfo\" style=\"border-left: solid #0087c3 5px;padding-left: 1%;\">" +
                        "<h2 id=\"PastryName\" style=\"padding: 0px;margin: 0px; font-size: 22px;\">Remarque</h2>" +
                        "<div id=\"PastryEmailDiv\" style=\"padding-left: 5%;\">" +
                            "Connecter vous à <strong>Kmandili</strong> pour faire une action." +
                        "</div>" +
                    "</div>" +
                "</div>";

        [AllowAnonymous]
        [Route("api/SendPasswordRestCode/{email}")]
        [HttpPost]
        public IHttpActionResult SendPasswordRestCode(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if ((db.Users.FirstOrDefault(u => u.Email == email) == null) &&
                (db.PastryShops.FirstOrDefault(p => p.Email == email) == null))
            {
                return NotFound();
            }
            string Code = Guid.NewGuid().ToString().Substring(0, 6);

            string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            string content = "<b>Code de réinitialisation de mot de passe</b> Date: " + date + "<br/>" +
                             "Copier le code si desous dans Kmandili<br/>" +
                             "<b>Ce code est valable pour 5 minnutes seulement!</b><br/>" + 
                             "<span style='font-size: 30px; color: black;'>" + Code + "</span>";

            MailMessage message = new MailMessage();
            message.Subject = "Kmandili: réinitialiser le mot de passe " + date;
            message.From = new MailAddress("kmandili.contact@gmail.com", "Kmandili");
            message.To.Add(new MailAddress(email));
            message.IsBodyHtml = true;
            message.Body = content;
            if (!SendEmail(message))
            {
                return BadRequest();
            }
            return Ok(Code);
        }
        
        [AllowAnonymous]
        [Route("api/sendEmailVerificationCode/{email}")]
        [HttpPost]
        public IHttpActionResult SendEmailVerificationCode(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string Code = Guid.NewGuid().ToString().Substring(0,6);

            string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            string content = "<b>Verification Email</b> Date: " + date + "<br/>" +
                             "Copier le code de verification si desous dans Kmandili<br/>" +
                             "<span style='font-size: 30px; color: black;'>" + Code + "</span>";
            
            MailMessage message = new MailMessage();
            message.Subject = "Kmandili: Verification email " + date;
            message.From = new MailAddress("kmandili.contact@gmail.com", "Kmandili");
            message.To.Add(new MailAddress(email));
            message.IsBodyHtml = true;
            message.Body = content;
            if (!SendEmail(message))
            {
                return BadRequest();
            }
            return Ok(Code);
        }

        [Route("api/sendOrderEmail/{id}")]
        [HttpGet]
        public IHttpActionResult SendOrderEmail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Order order = db.Orders.Find(id);
            if(!SendOrderEmailToUser(order, "") || !SendOrderEmailToPastryShop(order, "", NoticeString))
            {
                return BadRequest();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public void SendTimeOutCancelEmail(int id)
        {
            var order = db.Orders.Find(id);
            order.Status.StatusName = "Annulée";
            SendOrderEmailToUser(order,
                "Nous somme désolé de vous informer que votre commande a été annuler parcequ'elle a dépassé les delais d'attente.");
            SendOrderEmailToPastryShop(order, "Cette commande a été annuler a cause de votre retard de répense!", "");
        }

        [Route("api/sendCanelOrderEmail/{id}")]
        [HttpGet]
        public IHttpActionResult SendCancelOrderEmail(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Order order = db.Orders.Find(id);
            if (!SendOrderEmailToUser(order, "Commande Annulée") || !SendOrderEmailToPastryShop(order, "Commande Annulée", ""))
            {
                return BadRequest();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool SendOrderEmailToUser(Order order, string orderStatusMessage)
        {
            //string path = HttpContext.Current.Server.MapPath("~/Views/EmailViews/OrderViews/OrderEmailLayout.html");
            string path = HostingEnvironment.MapPath("~/Views/EmailViews/OrderViews/OrderEmailLayout.html");
            MailMessage message = new MailMessage();

            string content = File.ReadAllText(path);
            if (!orderStatusMessage.IsEmpty())
            {
                content = content.Replace("#OrderStatus", orderStatusMessage);
            }
            else
            {
                content = content.Replace("#OrderStatus", "Commande " + order.Status.StatusName);
            }
            message.Subject = "Kmandili: Commande " + order.Status.StatusName;
            content = content.Replace("#ToFrom", "à");
            content = content.Replace("#PastryName", order.PastryShop.Name);
            content = content.Replace("#PastryEmail", "<a href=\"mailto: " + order.PastryShop.Email + "\">" + order.PastryShop.Email + "</a>");
            content = content.Replace("#PastryAddress", order.PastryShop.Address.Number + ", " + order.PastryShop.Address.Street + ", " + order.PastryShop.Address.City + " " + order.PastryShop.Address.ZipCode + ", " + order.PastryShop.Address.State + ", " + order.PastryShop.Address.Country);
            content = content.Replace("#Notice", "");

            string phoneNumbers = "";
            foreach (PhoneNumber phoneNumber in order.PastryShop.PhoneNumbers)
            {
                phoneNumbers += "<a href=\"tel: " + phoneNumber.Number + "\">" + phoneNumber.Number + "</a><span class=\"PhoneNumberType\" style=\"margin - left: 3 %; \">" + phoneNumber.PhoneNumberType.Type + "</span><br>";
            }
            content = content.Replace("#PastryPhoneNumbers", phoneNumbers);
            content = content.Replace("#OrderNumber", order.ID.ToString());
            content = content.Replace("#OrderDate", order.Date.ToString("d"));
            content = content.Replace("#OrderDelevery", order.DeleveryMethod.DeleveryType);
            content = content.Replace("#OrderPayment", order.Payment.PaymentMethod);
            string productsRows = "";
            double Total = 0;
            foreach (OrderProduct orderProduct in order.OrderProducts)
            {
                string productCell = ProductCellTemplate;
                productCell = productCell.Replace("#ImgSRC", orderProduct.Product.Pic);
                productCell = productCell.Replace("#ProductName", orderProduct.Product.Name);
                productCell = productCell.Replace("#ProductDescription", orderProduct.Product.Description);
                productCell = productCell.Replace("#ProductPrice", orderProduct.Product.Price.ToString() + " TND");
                productCell = productCell.Replace("#ProductQuantity", orderProduct.Quantity.ToString());
                productCell = productCell.Replace("#ProductTotal", (orderProduct.Quantity * orderProduct.Product.Price).ToString() + " TND");
                Total += (orderProduct.Quantity * orderProduct.Product.Price);

                productsRows += productCell;
            }

            content = content.Replace("#OrderProducts", productsRows);
            content = content.Replace("#OrderTotal", Total + " TND");


            
            message.From = new MailAddress("kmandili.contact@gmail.com", "Kmandili");
            message.To.Add(new MailAddress(order.User.Email, order.User.Name));
            message.IsBodyHtml = true;
            message.Body = content;
            return SendEmail(message);
        }

        private bool SendOrderEmailToPastryShop(Order order, string orderStatusMessage, string noticeString)
        {
            //string path = HttpContext.Current.Server.MapPath("~/Views/EmailViews/OrderViews/OrderEmailLayout.html");
            string path = HostingEnvironment.MapPath("~/Views/EmailViews/OrderViews/OrderEmailLayout.html");
            MailMessage message = new MailMessage();

            string content = File.ReadAllText(path);
            if (!orderStatusMessage.IsEmpty())
            {
                content = content.Replace("#OrderStatus", orderStatusMessage);
            }
            else
            {
                content = content.Replace("#OrderStatus", "Commande " + order.Status.StatusName);
                
            }
            message.Subject = "Kmandili: Commande " + order.Status.StatusName;
            content = content.Replace("#Notice", noticeString);
            content = content.Replace("#ToFrom", "de");
            content = content.Replace("#PastryName", order.User.Name + " " + order.User.LastName);
            content = content.Replace("#PastryEmail", "<a href=\"mailto: " + order.User.Email + "\">" + order.User.Email + "</a>");
            content = content.Replace("#PastryAddress", order.User.Address.Number + ", " + order.User.Address.Street + ", " + order.User.Address.City + " " + order.User.Address.ZipCode + ", " + order.User.Address.State + ", " + order.User.Address.Country);
            
            string phoneNumbers = "";
            foreach (PhoneNumber phoneNumber in order.User.PhoneNumbers)
            {
                phoneNumbers += "<a href=\"tel: " + phoneNumber.Number + "\">" + phoneNumber.Number + "</a><span class=\"PhoneNumberType\" style=\"margin-left: 3 %; \">" + phoneNumber.PhoneNumberType.Type + "</span><br>";
            }
            content = content.Replace("#PastryPhoneNumbers", phoneNumbers);
            content = content.Replace("#OrderNumber", order.ID.ToString());
            content = content.Replace("#OrderDate", order.Date.ToString("d"));
            content = content.Replace("#OrderDelevery", order.DeleveryMethod.DeleveryType);
            content = content.Replace("#OrderPayment", order.Payment.PaymentMethod);
            string productsRows = "";
            double Total = 0;
            foreach (OrderProduct orderProduct in order.OrderProducts)
            {
                string productCell = ProductCellTemplate;
                productCell = productCell.Replace("#ImgSRC", orderProduct.Product.Pic);
                productCell = productCell.Replace("#ProductName", orderProduct.Product.Name);
                productCell = productCell.Replace("#ProductDescription", orderProduct.Product.Description);
                productCell = productCell.Replace("#ProductPrice", orderProduct.Product.Price.ToString() + " TND");
                productCell = productCell.Replace("#ProductQuantity", orderProduct.Quantity.ToString());
                productCell = productCell.Replace("#ProductTotal", (orderProduct.Quantity * orderProduct.Product.Price).ToString() + " TND");
                Total += (orderProduct.Quantity * orderProduct.Product.Price);

                productsRows += productCell;
            }

            content = content.Replace("#OrderProducts", productsRows);
            content = content.Replace("#OrderTotal", Total.ToString() + " TND");
            
            message.From = new MailAddress("kmandili.contact@gmail.com", "Kmandili");
            message.To.Add(new MailAddress(order.PastryShop.Email, order.PastryShop.Name));
            message.IsBodyHtml = true;
            message.Body = content;
            return SendEmail(message);
        }

        private bool SendEmail(MailMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("kmandili.contact@gmail.com", "kmandili2016");
                client.EnableSsl = true;
                try
                {
                    client.Send(message);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
