using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Web;
using SecuroteckWebApplication.Models;

namespace SecuroteckWebApplication.Controllers
{
    public class UserController : ApiController
    { 
        [ActionName("new")]
        public IHttpActionResult GET([FromUri]string username)
        {
            // Check for a non-valid string
            if (username != "")
            {
                // Check DB for the user
                bool exists = UserDatabaseAccess.CheckUsername(username);

                if (exists)
                {
                    return Ok("True - User Does Exist! Did you mean to do a POST to create a new user?");
                }
            }
            return Ok("False - User Does Not Exist! Did you mean to do a POST to create a new user?");
        }

        [ActionName("new")]
        public IHttpActionResult POST([FromBody]string username)
        {
            if (username != null)
            {
                string api =  UserDatabaseAccess.AddNewUser(username);
                if(UserDatabaseAccess.CheckAPIKeyExists(username))
                {
                    return Ok(api);
                }
                else
                {
                    return Content(HttpStatusCode.Forbidden, "Oops. This username is already in use. Please try again with a new username.");                
                }
            }

            return BadRequest("Oops. Make sure your body contains a string with your username and your Content-Type is Content - Type:application / json");
        }

        [ActionName("removeuser")]
        [APIAuthorise]
        public IHttpActionResult DELETE([FromUri]string username)
        {
            if (HttpContext.Current.Request.Headers["apikey"] != null)
            {
                string apikey = HttpContext.Current.Request.Headers["apikey"].ToString();
                if (UserDatabaseAccess.CheckAPIKeyExists(apikey))
                {
                    //if api key is in database
                    if (UserDatabaseAccess.CheckAPIandNameExists(apikey, username))
                    {
                        //if api and usernamematch
                        return Ok(UserDatabaseAccess.RemoveUser(apikey));
                    }
                    return Ok("false");
                }
            }
            return Ok("false");
        }

        [ActionName("changerole")]
        [APIAuthorise]
        [AdminRole]
        public IHttpActionResult POST([FromBody]ChangeJSON body)
        {
            string username = body.username;
            string role = body.role;
            if (HttpContext.Current.Request.Headers["apikey"] != null)
            {
                string apikey = HttpContext.Current.Request.Headers["apikey"].ToString();
                if (UserDatabaseAccess.CheckAPIKeyExists(apikey))
                {
                   if(UserDatabaseAccess.UpdateRole(role, username))
                   {
                        return Ok("DONE");
                   }
                   else if(UserDatabaseAccess.CheckUsername(username))
                   {
                        return BadRequest("NOT DONE: Role does not exist");
                   }
                    return BadRequest("NOT DONE: An error occured");

                }
                return BadRequest("NOT DONE: An error occured");
            }
            return BadRequest("NOT DONE: An error occured");
        }
    }
}