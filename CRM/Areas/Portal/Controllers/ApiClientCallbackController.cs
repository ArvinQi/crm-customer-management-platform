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
    public class ApiClientCallbackController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiClientCallback
        public IEnumerable<tb_ClientCallback> Gettb_ClientCallback()
        {
            var tb_clientcallback = db.tb_ClientCallback.Include(t => t.tb_Clients).Include(t => t.tb_Products).Include(t => t.tb_Users);
            return tb_clientcallback.AsEnumerable();
        }

        // GET api/ApiClientCallback/5
        public tb_ClientCallback Gettb_ClientCallback(long id)
        {
            tb_ClientCallback tb_clientcallback = db.tb_ClientCallback.Find(id);
            if (tb_clientcallback == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_clientcallback;
        }

        // PUT api/ApiClientCallback/5
        public HttpResponseMessage Puttb_ClientCallback(long id, tb_ClientCallback tb_clientcallback)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_clientcallback.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_clientcallback).State = EntityState.Modified;

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

        // POST api/ApiClientCallback
        public HttpResponseMessage Posttb_ClientCallback(tb_ClientCallback tb_clientcallback)
        {
            if (ModelState.IsValid)
            {
                db.tb_ClientCallback.Add(tb_clientcallback);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_clientcallback);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_clientcallback.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiClientCallback/5
        public HttpResponseMessage Deletetb_ClientCallback(long id)
        {
            tb_ClientCallback tb_clientcallback = db.tb_ClientCallback.Find(id);
            if (tb_clientcallback == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_ClientCallback.Remove(tb_clientcallback);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_clientcallback);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}