using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UsersDataAcess;
namespace UserService.Controllers
{
    public class UsersController : ApiController
    {
        public IEnumerable<User> Get()
        {
            using (tamedEntities tamedEntities = new tamedEntities())
            {
                return tamedEntities.Users.ToList();
            }

        }
        public HttpResponseMessage Get(int id)
        {
            using (tamedEntities tamedEntities = new tamedEntities())
            {
                var data = tamedEntities.Users.FirstOrDefault(e => e.Id == id);
                if (data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user wit id=" + id.ToString() + " NotFound");

                }
            }

        }
        public HttpResponseMessage Post([FromBody]User user)
        {
            try
            {
                using (tamedEntities tamedEntities = new tamedEntities())
                {
                    tamedEntities.Users.Add(user);
                    tamedEntities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri + user.Id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        public HttpResponseMessage Delete(int id)
        {
            using (tamedEntities tamedEntities = new tamedEntities())
            {
                try
                {
                    var data = tamedEntities.Users.FirstOrDefault(e => e.Id == id);
                    if (data != null)
                    {
                        tamedEntities.Users.Remove(data);
                        tamedEntities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "data with id =" + id.ToString() + "not avaalalble");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        public HttpResponseMessage put(int id, [FromBody] User user)
        {
            try
            {
                using (tamedEntities entities = new tamedEntities())
                {
                    var entity = entities.Users.FirstOrDefault(e => e.Id == id);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with id=" + id.ToString() + "not exist");
                    }
                    else
                    {
                        entity.Fname = user.Fname;
                        entity.Lname = user.Lname;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }
    }
}
