using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Http;
using System.Web.Http.Results;


namespace SecuroteckWebApplication.Models
{
    public class User
    {
        #region Task2
        // TODO: Create a User Class for use with Entity Framework
        // Note that you can use the [key] attribute to set your ApiKey Guid as the primary key 
        #endregion
        [Key]
        public string APIKey { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public User() { }
    }

    #region Task13?
    // TODO: You may find it useful to add code here for Logging
    #endregion

    public static class UserDatabaseAccess
    {
        #region Task3 
        // TODO: Make methods which allow us to read from/write to the database 
        #endregion

        public static string AddNewUser(string name)
        {
            bool exists = CheckUsername(name);
            string role;
            if (!exists)

            {
                using (var ctx = new UserContext())
                {
                    string key = Guid.NewGuid().ToString();
                    if (ctx.Users.Count() == 0) { role = "Admin"; }
                    else { role = "User"; }
                    User user = new User
                    {
                        UserName = name,
                        APIKey = key,
                        Role = role
                    };

                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    return key;
                }
            }
            return "Oops. This username is already in use. Please try again with a new username.";
        }

        public static bool CheckAPIKeyExists(string APIKey)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.APIKey == APIKey);

                if (user != null) { return true; }
                return false;
            }
        }

        public static bool CheckAPIandNameExists(string APIKey, string name)
        {
            using (var ctx = new UserContext())
            {
                var userapi = ctx.Users.FirstOrDefault(u => u.APIKey == APIKey);
                //var username = ctx.Users.FirstOrDefault(u => u.UserName == name);
                if (userapi != null && userapi.UserName == name) { return true; }
                return false;


            }
        }

        public static User CheckAPIReturnUser(string APIKey)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.APIKey == APIKey);
                if (user != null) { return user; }
                return user;
            }
        }

        public static string CheckAPIReturnName(string APIKey)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.APIKey == APIKey);
                if (user != null) { return user.UserName; }
                return user.UserName;
            }
        }

        public static bool CheckUsername(string username)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.UserName == username);
                if (user != null) { return true; }
                return false;
            }
        }

        public static bool RemoveUser(string apiKey)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.APIKey == apiKey);
                if (user != null)
                {
                    ctx.Users.Remove(user);
                    ctx.SaveChanges();
                    if (!CheckAPIKeyExists(apiKey))
                        return true;
                }
                return false;
            }
        }

        public static bool UpdateRole(string role, string username)
        {
            using (var ctx = new UserContext())
            {
                var user = ctx.Users.FirstOrDefault(u => u.UserName == username);
                if (user != null)
                {
                    if (role == "Admin" || role == "User")
                    {
                        user.Role = role;
                        ctx.SaveChanges();
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
    }
}
