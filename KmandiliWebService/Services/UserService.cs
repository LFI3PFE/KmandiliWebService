using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KmandiliDataAccess;
using KmandiliWebService.Models;

namespace KmandiliWebService.Services
{
    public class UserService
    {
        public ServerUser GetUserByCredentials(string email, string password)
        {
            var dbEntities = new KmandiliDBEntities();
            ServerUser serverUser = null;
            var user = dbEntities.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                serverUser = new ServerUser()
                {
                    ID = user.ID,
                    Email = user.Email,
                    Password = string.Empty,
                    Name = user.Name,
                    Type = "u"
                };
            }
            else
            {
                var pastryShop = dbEntities.PastryShops.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (pastryShop != null)
                {
                    serverUser = new ServerUser()
                    {
                        ID = pastryShop.ID,
                        Email = pastryShop.Email,
                        Password = string.Empty,
                        Name = pastryShop.Name,
                        Type = "p"
                    };
                }
            }
            return serverUser;
        }
    }
}