using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CRM.Models;

namespace CRM.Controllers
{
    public class LogonController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/Logon
        public IEnumerable<tb_Users> Gettb_Users()
        {
            var tb_users = db.tb_Users.Include(t => t.tb_Departs).Include(t => t.tb_Duties);
            return tb_users.AsEnumerable();
        }

        // GET api/Logon/5
        public tb_Users Gettb_Users(long id)
        {
            tb_Users tb_users = db.tb_Users.Find(id);
            if (tb_users == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_users;
        }

        // PUT api/Logon/5
        public HttpResponseMessage Puttb_Users(long id, tb_Users tb_users)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_users.UserID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Logon
        public HttpResponseMessage Posttb_Users(tb_Users tb_users)
        {
            if (ModelState.IsValid)
            {
                db.tb_Users.Add(tb_users);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_users);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_users.UserID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Logon/5
        public HttpResponseMessage Deletetb_Users(long id)
        {
            tb_Users tb_users = db.tb_Users.Find(id);
            if (tb_users == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_Users.Remove(tb_users);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_users);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}