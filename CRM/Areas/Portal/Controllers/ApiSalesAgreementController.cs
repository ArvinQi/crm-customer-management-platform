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
    public class ApiSalesAgreementController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/SalesAgreement
        public IEnumerable<tb_SalesAgreement> Gettb_SalesAgreement()
        {
            var tb_salesagreement = db.tb_SalesAgreement.Include(t => t.tb_Clients).Include(t => t.tb_Products).Include(t => t.tb_Users);
            return tb_salesagreement.AsEnumerable();
        }

        // GET api/SalesAgreement/5
        public tb_SalesAgreement Gettb_SalesAgreement(long id)
        {
            tb_SalesAgreement tb_salesagreement = db.tb_SalesAgreement.Find(id);
            if (tb_salesagreement == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_salesagreement;
        }

        // PUT api/SalesAgreement/5
        public HttpResponseMessage Puttb_SalesAgreement(long id, tb_SalesAgreement tb_salesagreement)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_salesagreement.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_salesagreement).State = EntityState.Modified;

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

        // POST api/SalesAgreement
        public HttpResponseMessage Posttb_SalesAgreement(tb_SalesAgreement tb_salesagreement)
        {
            if (ModelState.IsValid)
            {
                db.tb_SalesAgreement.Add(tb_salesagreement);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_salesagreement);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_salesagreement.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SalesAgreement/5
        public HttpResponseMessage Deletetb_SalesAgreement(long id)
        {
            tb_SalesAgreement tb_salesagreement = db.tb_SalesAgreement.Find(id);
            if (tb_salesagreement == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_SalesAgreement.Remove(tb_salesagreement);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_salesagreement);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}