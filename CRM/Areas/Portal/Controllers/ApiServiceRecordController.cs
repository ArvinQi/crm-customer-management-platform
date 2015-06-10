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
    public class ApiServiceRecordController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiServiceRecord
        public IEnumerable<tb_ServiceRecord> Gettb_ServiceRecord()
        {
            var tb_servicerecord = db.tb_ServiceRecord.Include(t => t.tb_Clients).Include(t => t.tb_Users);
            return tb_servicerecord.AsEnumerable();
        }

        // GET api/ApiServiceRecord/5
        public tb_ServiceRecord Gettb_ServiceRecord(long id)
        {
            tb_ServiceRecord tb_servicerecord = db.tb_ServiceRecord.Find(id);
            if (tb_servicerecord == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_servicerecord;
        }

        // PUT api/ApiServiceRecord/5
        public HttpResponseMessage Puttb_ServiceRecord(long id, tb_ServiceRecord tb_servicerecord)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_servicerecord.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_servicerecord).State = EntityState.Modified;

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

        // POST api/ApiServiceRecord
        public HttpResponseMessage Posttb_ServiceRecord(tb_ServiceRecord tb_servicerecord)
        {
            if (ModelState.IsValid)
            {
                db.tb_ServiceRecord.Add(tb_servicerecord);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_servicerecord);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_servicerecord.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiServiceRecord/5
        public HttpResponseMessage Deletetb_ServiceRecord(long id)
        {
            tb_ServiceRecord tb_servicerecord = db.tb_ServiceRecord.Find(id);
            if (tb_servicerecord == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_ServiceRecord.Remove(tb_servicerecord);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_servicerecord);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}