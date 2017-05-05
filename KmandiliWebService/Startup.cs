using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using KmandiliDataAccess;
using KmandiliWebService.Controllers.ApplicationControllers;
using Microsoft.Owin;
using Owin;
using WebGrease.Css.Extensions;

[assembly: OwinStartup(typeof(KmandiliWebService.Startup))]

namespace KmandiliWebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Task.Run(() => CheckMethod());
        }

        private static void CheckMethod()
        {
            while (true)
            {
                var x = TimeSpan.FromDays(1);
                Thread.Sleep(x);
                using (var db = new KmandiliDBEntities())
                {
                    var toCompareDate = DateTime.Now.Date;
                    var toDeleteOrders = db.Orders.Where(o => (o.Status_FK == 1) && (toCompareDate == DbFunctions.TruncateTime(DbFunctions.AddDays(o.Date, 2))));
                    toDeleteOrders.ForEach(SendCanceledEmail);
                    db.Orders.RemoveRange(toDeleteOrders);
                    db.SaveChanges();
                }
            }
        }

        private static void SendCanceledEmail(Order o)
        {
            using (var emailController = new EmailsController())
            {
                emailController.SendTimeOutCancelEmail(o.ID);
            }
        }
    }
}
