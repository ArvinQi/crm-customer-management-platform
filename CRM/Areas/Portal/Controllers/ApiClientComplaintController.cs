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
    public class ApiClientComplaintController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiClientComplaint
        public IEnumerable<tb_ClientComplaint> Gettb_ClientComplaint()
        {
            var tb_clientcomplaint = db.tb_ClientComplaint.Include(t => t.tb_Clients).Include(t => t.tb_Products).Include(t => t.tb_Users);
            return tb_clientcomplaint.AsEnumerable();
        }

        // GET api/ApiClientComplaint/5
        public tb_ClientComplaint Gettb_ClientComplaint(long id)
        {
            tb_ClientComplaint tb_clientcomplaint = db.tb_ClientComplaint.Find(id);
            if (tb_clientcomplaint == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_clientcomplaint;
        }

        // PUT api/ApiClientComplaint/5
        public HttpResponseMessage Puttb_ClientComplaint(long id, tb_ClientComplaint tb_clientcomplaint)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_clientcomplaint.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_clientcomplaint).State = EntityState.Modified;

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

        // POST api/ApiClientComplaint
        public HttpResponseMessage Posttb_ClientComplaint(tb_ClientComplaint tb_clientcomplaint)
        {
            if (ModelState.IsValid)
            {
                db.tb_ClientComplaint.Add(tb_clientcomplaint);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_clientcomplaint);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_clientcomplaint.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiClientComplaint/5
        public HttpResponseMessage Deletetb_ClientComplaint(long id)
        {
            tb_ClientComplaint tb_clientcomplaint = db.tb_ClientComplaint.Find(id);
            if (tb_clientcomplaint == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_ClientComplaint.Remove(tb_clientcomplaint);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_clientcomplaint);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}