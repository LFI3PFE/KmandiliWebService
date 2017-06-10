using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using KmandiliDataAccess;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;

namespace KmandiliWebService.Controllers.ApplicationControllers
{
    public class ChartsController : ApiController
    {
        private class JSONDataObject
        {
            public JSONDataObject()
            {
                this.Labels = new List<string>();
                this.Values = new List<double>();
                this.Total = 0;
            }

            public int Total { get; set; }

            public List<string> Labels { get; set; }
            public List<double> Values { get; set; }
        }


        [Route("api/GetLineChartView/{id}")]
        public HttpResponseMessage GetLineChartView(int id)
        {
            string htmlPath = HostingEnvironment.MapPath("~/Views/Charts/Line/local.html");
            string chartJSPath = HostingEnvironment.MapPath("~/Views/Charts/Chart.js");
            string mainJSPath = HostingEnvironment.MapPath("~/Views/Charts/Line/Main.js");
            string bootstrapPath = HostingEnvironment.MapPath("~/Views/Charts/bootstrap.css");

            string chartJS = File.ReadAllText(chartJSPath);
            string mainJS = File.ReadAllText(mainJSPath);
            string bootstrap = File.ReadAllText(bootstrapPath);
            string html = File.ReadAllText(htmlPath);

            html = html.Replace("#BootStrap.css", bootstrap);
            html = html.Replace("#Chart.js", chartJS);
            html = html.Replace("#Main.js", mainJS);


            var db = new KmandiliDBEntities();
            PastryShop pastryShop = db.PastryShops.Find(id);
            if (pastryShop == null)
            {
                return null;
            }

            var ordersByMonth = pastryShop.Orders.GroupBy(o => o.Date.Month).Select(g => new { Month = g.Key, Count = g.Count() }).Take(6);
            var ordersByMonthJSON = new JSONDataObject();
            foreach (var orderByMonth in ordersByMonth)
            {
                ordersByMonthJSON.Labels.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(orderByMonth.Month));
                ordersByMonthJSON.Values.Add(orderByMonth.Count);
                ordersByMonthJSON.Total += orderByMonth.Count;
            }
            string json = JsonConvert.SerializeObject(ordersByMonthJSON);
            html = html.Replace("#lineDataJSON", json);

            var response = new HttpResponseMessage();
            byte[] byteArray = Encoding.UTF8.GetBytes(html);
            MemoryStream stream = new MemoryStream(byteArray);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");


            return response;
        }


        [Route("api/GetDoughnutChartView/{id}")]
        // GET: Charts
        public HttpResponseMessage GetDoughnutChartView(int id)
        {
            string htmlPath = HostingEnvironment.MapPath("~/Views/Charts/Doughnut/local.html");
            string chartJSPath = HostingEnvironment.MapPath("~/Views/Charts/Chart.js");
            string mainJSPath = HostingEnvironment.MapPath("~/Views/Charts/Doughnut/Main.js");
            string bootstrapPath = HostingEnvironment.MapPath("~/Views/Charts/bootstrap.css");

            string chartJS = File.ReadAllText(chartJSPath);
            string mainJS = File.ReadAllText(mainJSPath);
            string bootstrap = File.ReadAllText(bootstrapPath);
            string html= File.ReadAllText(htmlPath);

            html = html.Replace("#BootStrap.css", bootstrap);
            html = html.Replace("#Chart.js", chartJS);
            html = html.Replace("#Main.js", mainJS);


            var db = new KmandiliDBEntities();
            PastryShop pastryShop = db.PastryShops.Find(id);
            if (pastryShop == null)
            {
                return null;
            }
            var allOrders = (from product in db.Products
                from orderedProduct in db.OrderProducts
                from order in db.Orders
                where order.PastryShop_FK == id
                where orderedProduct.Order_FK == order.ID
                where orderedProduct.Product_FK == product.ID
                group orderedProduct by product
                into productGroups
                select new
                {
                    product = productGroups.Key,
                    numberOfOrders = productGroups.Count()
                }
                ).OrderByDescending(x => x.numberOfOrders);

            var jsonDataObject = new JSONDataObject();
            double sum = 0;
            jsonDataObject.Total = allOrders.Sum(p => p.numberOfOrders);
            var topFive = allOrders.Take(5);
            foreach (var orderProductCount in topFive)
            {
                jsonDataObject.Labels.Add(orderProductCount.product.Name);
                sum += orderProductCount.numberOfOrders;
                jsonDataObject.Values.Add(orderProductCount.numberOfOrders);
            }
            jsonDataObject.Labels.Add("Autres");
            jsonDataObject.Values.Add(jsonDataObject.Total - sum);
            string jsonData = JsonConvert.SerializeObject(jsonDataObject);
            html = html.Replace("#doughnutDataJSON", jsonData);


            var response = new HttpResponseMessage();
            byte[] byteArray = Encoding.UTF8.GetBytes(html);
            MemoryStream stream = new MemoryStream(byteArray);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");


            return response;
        }
    }
}