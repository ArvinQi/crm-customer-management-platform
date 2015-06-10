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
    public class ApiSalesPlanController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/SalesPlan
        public IEnumerable<tb_SalesPlan> Gettb_SalesPlan()
        {
            var tb_salesplan = db.tb_SalesPlan.Include(t => t.tb_Departs).Include(t => t.tb_Users);
            return tb_salesplan.AsEnumerable();
        }

        // GET api/SalesPlan/5
        public tb_SalesPlan Gettb_SalesPlan(long id)
        {
            tb_SalesPlan tb_salesplan = db.tb_SalesPlan.Find(id);
            if (tb_salesplan == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_salesplan;
        }

        // PUT api/SalesPlan/5
        public HttpResponseMessage Puttb_SalesPlan(long id, tb_SalesPlan tb_salesplan)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_salesplan.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_salesplan).State = EntityState.Modified;

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

        // POST api/SalesPlan
        public HttpResponseMessage Posttb_SalesPlan(tb_SalesPlan tb_salesplan)
        {
            if (ModelState.IsValid)
            {
                db.tb_SalesPlan.Add(tb_salesplan);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_salesplan);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_salesplan.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SalesPlan/5
        public HttpResponseMessage Deletetb_SalesPlan(long id)
        {
            tb_SalesPlan tb_salesplan = db.tb_SalesPlan.Find(id);
            if (tb_salesplan == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_SalesPlan.Remove(tb_salesplan);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_salesplan);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}