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

namespace CRM.Areas.Admin.Controllers
{
    public class ApiDutiesController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiDuties
        public IEnumerable<tb_Duties> Gettb_Duties()
        {
            var tb_duties = db.tb_Duties.Include(t => t.tb_Departs);
            return tb_duties.AsEnumerable();
        }

        // GET api/ApiDuties/5
        public tb_Duties Gettb_Duties(long id)
        {
            tb_Duties tb_duties = db.tb_Duties.Find(id);
            if (tb_duties == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_duties;
        }

        // PUT api/ApiDuties/5
        public HttpResponseMessage Puttb_Duties(long id, tb_Duties tb_duties)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_duties.DutyID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_duties).State = EntityState.Modified;

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

        // POST api/ApiDuties
        public HttpResponseMessage Posttb_Duties(tb_Duties tb_duties)
        {
            if (ModelState.IsValid)
            {
                db.tb_Duties.Add(tb_duties);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_duties);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_duties.DutyID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiDuties/5
        public HttpResponseMessage Deletetb_Duties(long id)
        {
            tb_Duties tb_duties = db.tb_Duties.Find(id);
            if (tb_duties == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_Duties.Remove(tb_duties);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_duties);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}