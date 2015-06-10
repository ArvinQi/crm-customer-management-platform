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
    public class ApiClientContactController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiClientContact
        public IEnumerable<tb_ClientContactRecord> Gettb_ClientContactRecord()
        {
            var tb_clientcontactrecord = db.tb_ClientContactRecord.Include(t => t.tb_Clients).Include(t => t.tb_Users);
            return tb_clientcontactrecord.AsEnumerable();
        }

        // GET api/ApiClientContact/5
        public tb_ClientContactRecord Gettb_ClientContactRecord(long id)
        {
            tb_ClientContactRecord tb_clientcontactrecord = db.tb_ClientContactRecord.Find(id);
            if (tb_clientcontactrecord == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_clientcontactrecord;
        }

        // PUT api/ApiClientContact/5
        public HttpResponseMessage Puttb_ClientContactRecord(long id, tb_ClientContactRecord tb_clientcontactrecord)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_clientcontactrecord.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_clientcontactrecord).State = EntityState.Modified;

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

        // POST api/ApiClientContact
        public HttpResponseMessage Posttb_ClientContactRecord(tb_ClientContactRecord tb_clientcontactrecord)
        {
            if (ModelState.IsValid)
            {
                db.tb_ClientContactRecord.Add(tb_clientcontactrecord);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_clientcontactrecord);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_clientcontactrecord.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiClientContact/5
        public HttpResponseMessage Deletetb_ClientContactRecord(long id)
        {
            tb_ClientContactRecord tb_clientcontactrecord = db.tb_ClientContactRecord.Find(id);
            if (tb_clientcontactrecord == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_ClientContactRecord.Remove(tb_clientcontactrecord);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_clientcontactrecord);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}