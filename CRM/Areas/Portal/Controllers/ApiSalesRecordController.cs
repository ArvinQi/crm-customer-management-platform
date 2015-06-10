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
    public class ApiSalesRecordController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/SalesRecord
        public IEnumerable<tb_SalesRecord> Gettb_SalesRecord()
        {
            var tb_salesrecord = db.tb_SalesRecord.Include(t => t.tb_Clients).Include(t => t.tb_Products).Include(t => t.tb_Users);
            return tb_salesrecord.AsEnumerable();
        }

        // GET api/SalesRecord/5
        public tb_SalesRecord Gettb_SalesRecord(long id)
        {
            tb_SalesRecord tb_salesrecord = db.tb_SalesRecord.Find(id);
            if (tb_salesrecord == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_salesrecord;
        }

        // PUT api/SalesRecord/5
        public HttpResponseMessage Puttb_SalesRecord(long id, tb_SalesRecord tb_salesrecord)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_salesrecord.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_salesrecord).State = EntityState.Modified;

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

        // POST api/SalesRecord
        public HttpResponseMessage Posttb_SalesRecord(tb_SalesRecord tb_salesrecord)
        {
            if (ModelState.IsValid)
            {
                db.tb_SalesRecord.Add(tb_salesrecord);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_salesrecord);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_salesrecord.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SalesRecord/5
        public HttpResponseMessage Deletetb_SalesRecord(long id)
        {
            tb_SalesRecord tb_salesrecord = db.tb_SalesRecord.Find(id);
            if (tb_salesrecord == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_SalesRecord.Remove(tb_salesrecord);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_salesrecord);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}