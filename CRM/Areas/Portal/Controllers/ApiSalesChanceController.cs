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
    public class ApiSalesChanceController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/SalesChance
        public IEnumerable<tb_SalesChance> Gettb_SalesChance()
        {
            var tb_saleschance = db.tb_SalesChance.Include(t => t.tb_Clients).Include(t => t.tb_Products).Include(t => t.tb_Users);
            return tb_saleschance.AsEnumerable();
        }

        // GET api/SalesChance/5
        public tb_SalesChance Gettb_SalesChance(long id)
        {
            tb_SalesChance tb_saleschance = db.tb_SalesChance.Find(id);
            if (tb_saleschance == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_saleschance;
        }

        // PUT api/SalesChance/5
        public HttpResponseMessage Puttb_SalesChance(long id, tb_SalesChance tb_saleschance)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_saleschance.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_saleschance).State = EntityState.Modified;

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

        // POST api/SalesChance
        public HttpResponseMessage Posttb_SalesChance(tb_SalesChance tb_saleschance)
        {
            if (ModelState.IsValid)
            {
                db.tb_SalesChance.Add(tb_saleschance);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_saleschance);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_saleschance.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SalesChance/5
        public HttpResponseMessage Deletetb_SalesChance(long id)
        {
            tb_SalesChance tb_saleschance = db.tb_SalesChance.Find(id);
            if (tb_saleschance == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_SalesChance.Remove(tb_saleschance);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_saleschance);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}