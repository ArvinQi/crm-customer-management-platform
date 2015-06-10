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

namespace CRM.Areas.Portal.Controllers
{
    public class ApiClientCareController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiClientCare
        public IEnumerable<tb_ClientCare> Gettb_ClientCare()
        {
            var tb_clientcare = db.tb_ClientCare.Include(t => t.tb_Clients).Include(t => t.tb_Users);
            return tb_clientcare.AsEnumerable();
        }

        // GET api/ApiClientCare/5
        public tb_ClientCare Gettb_ClientCare(long id)
        {
            tb_ClientCare tb_clientcare = db.tb_ClientCare.Find(id);
            if (tb_clientcare == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_clientcare;
        }

        // PUT api/ApiClientCare/5
        public HttpResponseMessage Puttb_ClientCare(long id, tb_ClientCare tb_clientcare)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_clientcare.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_clientcare).State = EntityState.Modified;

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

        // POST api/ApiClientCare
        public HttpResponseMessage Posttb_ClientCare(tb_ClientCare tb_clientcare)
        {
            if (ModelState.IsValid)
            {
                db.tb_ClientCare.Add(tb_clientcare);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_clientcare);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_clientcare.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiClientCare/5
        public HttpResponseMessage Deletetb_ClientCare(long id)
        {
            tb_ClientCare tb_clientcare = db.tb_ClientCare.Find(id);
            if (tb_clientcare == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_ClientCare.Remove(tb_clientcare);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_clientcare);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}