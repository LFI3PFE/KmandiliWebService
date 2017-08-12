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
    //[Authorize]
    public class ChartsController : ApiController
    {
        private class JSONDataObject
        {
            public JSONDataObject()
            {
                this.Labels = new List<string>();
                this.Values = new List<double>();
                this.Total = 0;
                this.Year = 2005;
            }

            public double Total { get; set; }
            public int Year { get; set; }

            public List<string> Labels { get; set; }
            public List<double> Values { get; set; }
        }

        [Route("api/GetProductChartView/{id}/{year}/{sem}")]
        public HttpResponseMessage GetProductCharView(int id, int year, int sem)
        {
            string htmlPath = HostingEnvironment.MapPath("~/Views/Charts/Product/local.html");
            string chartJSPath = HostingEnvironment.MapPath("~/Views/Charts/Chart.js");
            string mainJSPath = HostingEnvironment.MapPath("~/Views/Charts/Product/Main.js");
            string bootstrapPath = HostingEnvironment.MapPath("~/Views/Charts/bootstrap.css");

            string chartJS = File.ReadAllText(chartJSPath);
            string mainJS = File.ReadAllText(mainJSPath);
            string bootstrap = File.ReadAllText(bootstrapPath);
            string html = File.ReadAllText(htmlPath);

            html = html.Replace("#BootStrap.css", bootstrap);
            html = html.Replace("#Chart.js", chartJS);
            html = html.Replace("#Main.js", mainJS);


            var db = new KmandiliDBEntities();
            var product = db.Products.Find(id);
            if (product == null)
            {
                var x = new HttpResponseMessage();
                x.StatusCode = HttpStatusCode.NotFound;
                return x;
            }

            DateTime start, end;
            if (sem == 1)
            {
                start = new DateTime(year, 1, 1);
                end = new DateTime(year, 6, 30);
            }
            else
            {
                start = new DateTime(year, 7, 1);
                end = new DateTime(year, 12, 31);
            }
            var months = new List<DateTime>();
            months.Add(start);
            while ((months.Last().Year != end.Year) || (months.Last().Month != end.Month))
            {
                months.Add(months.Last().AddMonths(1));
            }

            var ordersByMonth =
                months.GroupJoin(product.OrderProducts,
                    m => new { month = m.Month, year = m.Year },
                    orderProduct => new {
                        month = orderProduct.Order.Date.Month,
                        year = orderProduct.Order.Date.Year
                    },
                    (p, g) => new {
                        month = p.Month,
                        year = p.Year,
                        count = g.Count()
                    });

            var quantityJSON = new JSONDataObject();
            foreach (var month in months)
            {
                var p = product.OrderProducts.Where(
                    op => op.Order.Date.Month == month.Month && op.Order.Date.Year == month.Year)
                    .GroupBy(op => new {op.Product, op.Order.Date.Month, op.Order.Date.Year})
                    .Select(
                        group =>
                            new
                            {
                                Product = group.Key.Product,
                                Sum = group.Sum(op => op.Quantity),
                                Year = group.Key.Year,
                                Month = group.Key.Month
                            });
                quantityJSON.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(month.Month).ToLower()));
                quantityJSON.Values.Add(p.Any()?p.First().Sum:0);
            }

            var ordersByMonthJSON = new JSONDataObject();
            ordersByMonthJSON.Year = year;
            foreach (var orderByMonth in ordersByMonth)
            {
                ordersByMonthJSON.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(orderByMonth.month).ToLower()));
                ordersByMonthJSON.Values.Add(orderByMonth.count);
                ordersByMonthJSON.Total += orderByMonth.count;
            }

            string ordersJson = JsonConvert.SerializeObject(ordersByMonthJSON);
            string quantityJson = JsonConvert.SerializeObject(quantityJSON);
            html = html.Replace("#productOrdersDataJSON", ordersJson);
            html = html.Replace("#productQunatityDataJSON", quantityJson);
            html = html.Replace("#Year", "Année: " + year.ToString());

            var response = new HttpResponseMessage();
            byte[] byteArray = Encoding.UTF8.GetBytes(html);
            MemoryStream stream = new MemoryStream(byteArray);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }


        [Route("api/GetLineChartView/{id}/{year}/{sem}")]
        public HttpResponseMessage GetLineChartView(int id, int year, int sem)
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
                var x = new HttpResponseMessage();
                x.StatusCode = HttpStatusCode.NotFound;
                return x;
            }

            DateTime start, end;
            if (sem == 1)
            {
                start = new DateTime(year, 1, 1);
                end = new DateTime(year, 6, 30);
            }
            else
            {
                start = new DateTime(year, 7, 1);
                end = new DateTime(year, 12, 31);
            }
            var months = new List<DateTime>();
            months.Add(start);
            while ((months.Last().Year != end.Year) || (months.Last().Month != end.Month))
            {
                months.Add(months.Last().AddMonths(1));
            }

            var ordersByMonth =
                months.GroupJoin(pastryShop.Orders,
                    m => new { month = m.Month, year = m.Year },
                    order => new {
                        month = order.Date.Month,
                        year = order.Date.Year
                    },
                    (p, g) => new {
                        month = p.Month,
                        year = p.Year,
                        count = g.Count()
                    });

            var ordersByMonthJSON = new JSONDataObject();
            ordersByMonthJSON.Year = year;
            foreach (var orderByMonth in ordersByMonth)
            {
                ordersByMonthJSON.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(orderByMonth.month).ToLower()));
                ordersByMonthJSON.Values.Add(orderByMonth.count);
                ordersByMonthJSON.Total += orderByMonth.count;
            }

            string json = JsonConvert.SerializeObject(ordersByMonthJSON);
            html = html.Replace("#lineDataJSON", json);
            html = html.Replace("#lineTitle", "Commandes");
            html = html.Replace("#Year", "Année: " + year.ToString());

            var response = new HttpResponseMessage();
            byte[] byteArray = Encoding.UTF8.GetBytes(html);
            MemoryStream stream = new MemoryStream(byteArray);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }

        [Route("api/GetNewUsersChartView/{year}/{sem}")]
        public HttpResponseMessage GetNewUsersChartView(int year, int sem)
        {
            string htmlPath = HostingEnvironment.MapPath("~/Views/Charts/User/local.html");
            string chartJSPath = HostingEnvironment.MapPath("~/Views/Charts/Chart.js");
            string mainJSPath = HostingEnvironment.MapPath("~/Views/Charts/User/Main.js");
            string bootstrapPath = HostingEnvironment.MapPath("~/Views/Charts/bootstrap.css");

            string chartJS = File.ReadAllText(chartJSPath);
            string mainJS = File.ReadAllText(mainJSPath);
            string bootstrap = File.ReadAllText(bootstrapPath);
            string html = File.ReadAllText(htmlPath);

            html = html.Replace("#BootStrap.css", bootstrap);
            html = html.Replace("#Chart.js", chartJS);
            html = html.Replace("#Main.js", mainJS);


            var db = new KmandiliDBEntities();
            var pastryShops = db.PastryShops;
            var clients = db.Users;
            if (pastryShops == null || clients == null)
            {
                var x = new HttpResponseMessage();
                x.StatusCode = HttpStatusCode.NotFound;
                return x;
            }

            DateTime start, end;
            if (sem == 1)
            {
                start = new DateTime(year, 1, 1);
                end = new DateTime(year, 6, 30);
            }
            else
            {
                start = new DateTime(year, 7, 1);
                end = new DateTime(year, 12, 31);
            }
            var months = new List<DateTime>();
            months.Add(start);
            while ((months.Last().Year != end.Year) || (months.Last().Month != end.Month))
            {
                months.Add(months.Last().AddMonths(1));
            }

            var pastryShopsByMonth =
                months.GroupJoin(pastryShops,
                    m => new {month = m.Month, year = m.Year},
                    pastryShop => new
                    {
                        month = pastryShop.JoinDate.Value.Month,
                        year = pastryShop.JoinDate.Value.Year
                    },
                    (p, g) => new
                    {
                        month = p.Month,
                        year = p.Year,
                        count = g.Count()
                    });

            var clientsByMonth =
                months.GroupJoin(clients,
                    m => new {month = m.Month, year = m.Year},
                    client => new
                    {
                        month = client.JoinDate.Month,
                        year = client.JoinDate.Year
                    },
                    (p, g) => new
                    {
                        month = p.Month,
                        year = p.Year,
                        count = g.Count()
                    });

            var usersByMonth = clientsByMonth.GroupJoin(pastryShops,
                m => new {mm = m.month, yy = m.year},
                pastryShop => new
                {
                    mm = pastryShop.JoinDate.Value.Month,
                    yy = pastryShop.JoinDate.Value.Year,
                },
                (p, g) => new
                {
                    mm = p.month,
                    yy = p.year,
                    count = p.count + g.Count()
                });

            var clientsJsonObject = new JSONDataObject();
            clientsJsonObject.Year = year;
            foreach (var clientMonth in clientsByMonth)
            {
                clientsJsonObject.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(clientMonth.month).ToLower()));
                clientsJsonObject.Values.Add(clientMonth.count);
                clientsJsonObject.Total += clientMonth.count;
            }

            var pastryShopsJsonObject = new JSONDataObject();
            pastryShopsJsonObject.Year = year;
            foreach (var pastryShopMonth in pastryShopsByMonth)
            {
                pastryShopsJsonObject.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(pastryShopMonth.month).ToLower()));
                pastryShopsJsonObject.Values.Add(pastryShopMonth.count);
                pastryShopsJsonObject.Total += pastryShopMonth.count;
            }

            var usersJsonObject = new JSONDataObject();
            usersJsonObject.Year = year;
            foreach (var userMonth in usersByMonth)
            {
                usersJsonObject.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(userMonth.mm).ToLower()));
                usersJsonObject.Values.Add(userMonth.count);
                usersJsonObject.Total += userMonth.count;
            }

            string clientsJson = JsonConvert.SerializeObject(clientsJsonObject);
            string pastryShopsJson = JsonConvert.SerializeObject(pastryShopsJsonObject);
            string usersJson = JsonConvert.SerializeObject(usersJsonObject);
            html = html.Replace("#clientDataJSON", clientsJson);
            html = html.Replace("#pastryDataJSON", pastryShopsJson);
            html = html.Replace("#userDataJSON", usersJson);
            html = html.Replace("#Year", "Année: " + year.ToString());

            var response = new HttpResponseMessage();
            byte[] byteArray = Encoding.UTF8.GetBytes(html);
            MemoryStream stream = new MemoryStream(byteArray);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }

        [Route("api/GetUsersChartView/{year}/{sem}")]
        public HttpResponseMessage GetUsersChartView(int year, int sem)
        {
            string htmlPath = HostingEnvironment.MapPath("~/Views/Charts/User/local.html");
            string chartJSPath = HostingEnvironment.MapPath("~/Views/Charts/Chart.js");
            string mainJSPath = HostingEnvironment.MapPath("~/Views/Charts/User/Main.js");
            string bootstrapPath = HostingEnvironment.MapPath("~/Views/Charts/bootstrap.css");

            string chartJS = File.ReadAllText(chartJSPath);
            string mainJS = File.ReadAllText(mainJSPath);
            string bootstrap = File.ReadAllText(bootstrapPath);
            string html = File.ReadAllText(htmlPath);

            html = html.Replace("#BootStrap.css", bootstrap);
            html = html.Replace("#Chart.js", chartJS);
            html = html.Replace("#Main.js", mainJS);


            var db = new KmandiliDBEntities();
            var pastryShops = db.PastryShops;
            var clients = db.Users;
            if (pastryShops == null || clients == null)
            {
                var x = new HttpResponseMessage();
                x.StatusCode = HttpStatusCode.NotFound;
                return x;
            }

            DateTime start, end;
            if (sem == 1)
            {
                start = new DateTime(year, 1, 1);
                end = new DateTime(year, 6, 30);
            }
            else
            {
                start = new DateTime(year, 7, 1);
                end = new DateTime(year, 12, 31);
            }
            var months = new List<DateTime>();
            months.Add(start);
            while ((months.Last().Year != end.Year) || (months.Last().Month != end.Month))
            {
                months.Add(months.Last().AddMonths(1));
            }

            var pastryShopsByMonth =
                months.ToList().GroupJoin(pastryShops,
                    m => new {month = m.Month, year = m.Year},
                    pastryShop => new
                    {
                        month = pastryShop.JoinDate.Value.Month,
                        year = pastryShop.JoinDate.Value.Year
                    },
                    (p, g) => new
                    {
                        month = p.Month,
                        year = p.Year,
                        count =
                            g.Count() +
                            pastryShops.Count(
                                ps =>
                                    (DbFunctions.TruncateTime(DbFunctions.AddDays(ps.JoinDate.Value,
                                        -ps.JoinDate.Value.Day + 1)).Value <=
                                     DbFunctions.AddMonths(DbFunctions.CreateDateTime(p.Year, p.Month, 1, 0, 0, 0), -1)
                                         .Value)
                                )
                    });

            var clientsByMonth =
                months.GroupJoin(clients,
                    m => new {month = m.Month, year = m.Year},
                    client => new
                    {
                        month = client.JoinDate.Month,
                        year = client.JoinDate.Year
                    },
                    (p, g) => new
                    {
                        month = p.Month,
                        year = p.Year,
                        count =
                            g.Count() +
                            clients.Count(c => (DbFunctions.TruncateTime(DbFunctions.AddDays(c.JoinDate,
                                -c.JoinDate.Day + 1)).Value <=
                                                DbFunctions.AddMonths(
                                                    DbFunctions.CreateDateTime(p.Year, p.Month, 1, 0, 0, 0), -1)
                                                    .Value))
                    });

            var usersByMonth =
                clientsByMonth.SelectMany(clientByMonth => pastryShopsByMonth,
                    (clientByMonth, pastryShopByMonth) => new {clientByMonth, pastryShopByMonth})
                    .Where(
                        @t =>
                            (@t.clientByMonth.month == @t.pastryShopByMonth.month &&
                             @t.clientByMonth.year == @t.pastryShopByMonth.year))
                    .Select(@t => new
                    {
                        month = @t.clientByMonth.month,
                        year = @t.clientByMonth.year,
                        count = @t.clientByMonth.count + @t.pastryShopByMonth.count
                    });

            var clientsJsonObject = new JSONDataObject();
            clientsJsonObject.Year = year;
            foreach (var clientMonth in clientsByMonth)
            {
                clientsJsonObject.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(clientMonth.month).ToLower()));
                clientsJsonObject.Values.Add(clientMonth.count);
                clientsJsonObject.Total += clientMonth.count;
            }

            var pastryShopsJsonObject = new JSONDataObject();
            pastryShopsJsonObject.Year = year;
            foreach (var pastryShopMonth in pastryShopsByMonth)
            {
                pastryShopsJsonObject.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(pastryShopMonth.month).ToLower()));
                pastryShopsJsonObject.Values.Add(pastryShopMonth.count);
                pastryShopsJsonObject.Total += pastryShopMonth.count;
            }

            var usersJsonObject = new JSONDataObject();
            usersJsonObject.Year = year;
            foreach (var userMonth in usersByMonth)
            {
                usersJsonObject.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(userMonth.month).ToLower()));
                usersJsonObject.Values.Add(userMonth.count);
                usersJsonObject.Total += userMonth.count;
            }

            string clientsJson = JsonConvert.SerializeObject(clientsJsonObject);
            string pastryShopsJson = JsonConvert.SerializeObject(pastryShopsJsonObject);
            string usersJson = JsonConvert.SerializeObject(usersJsonObject);
            html = html.Replace("#clientDataJSON", clientsJson);
            html = html.Replace("#pastryDataJSON", pastryShopsJson);
            html = html.Replace("#userDataJSON", usersJson);
            html = html.Replace("#Year", "Année: " + year.ToString());

            var response = new HttpResponseMessage();
            byte[] byteArray = Encoding.UTF8.GetBytes(html);
            MemoryStream stream = new MemoryStream(byteArray);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }

        [Route("api/GetIncomsChartView/{id}/{year}/{sem}")]
        public HttpResponseMessage GetIncomsChartView(int id, int year, int sem)
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
                var x = new HttpResponseMessage();
                x.StatusCode = HttpStatusCode.NotFound;
                return x;
            }

            DateTime start, end;
            if (sem == 1)
            {
                start = new DateTime(year, 1, 1);
                end = new DateTime(year, 6, 30);
            }
            else
            {
                start = new DateTime(year, 7, 1);
                end = new DateTime(year, 12, 31);
            }
            var months = new List<DateTime>();
            months.Add(start);
            while ((months.Last().Year != end.Year) || (months.Last().Month != end.Month))
            {
                months.Add(months.Last().AddMonths(1));
            }

            var ordersByMonth =
                months.GroupJoin(pastryShop.Orders,
                    m => new { month = m.Month, year = m.Year},
                    order => new {
                        month = order.Date.Month,
                        year = order.Date.Year
                    },
                    (p, g) => new {
                        month = p.Month,
                        year = p.Year,
                        incoms = g.Where(o => o.Status_FK == 5).Sum(o => (o.OrderProducts.Sum(op => (op.Quantity * op.Product.Price)))),
                        count = g.Count()
                    });

            var ordersByMonthJSON = new JSONDataObject();
            ordersByMonthJSON.Year = year;
            foreach (var orderByMonth in ordersByMonth)
            {
                ordersByMonthJSON.Labels.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(new CultureInfo("fr-FR").DateTimeFormat.GetMonthName(orderByMonth.month).ToLower()));
                ordersByMonthJSON.Values.Add(orderByMonth.incoms);
                ordersByMonthJSON.Total += orderByMonth.incoms;
            }

            string json = JsonConvert.SerializeObject(ordersByMonthJSON);
            html = html.Replace("#lineDataJSON", json);
            html = html.Replace("#lineTitle", "Dinar Tunisien");
            html = html.Replace("#Year", "Année: " + year.ToString());

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
                var x = new HttpResponseMessage();
                x.StatusCode = HttpStatusCode.NotFound;
                return x;
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
            jsonDataObject.Total = allOrders.Any()?allOrders.Sum(p => p.numberOfOrders):0;
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