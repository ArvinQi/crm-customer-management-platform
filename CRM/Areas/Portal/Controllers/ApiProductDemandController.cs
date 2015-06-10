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
    public class ApiProductDemandController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiProductDemand
        public IEnumerable<tb_Demands> Gettb_Demands()
        {
            var tb_demands = db.tb_Demands.Include(t => t.tb_Products).Include(t => t.tb_Users);
            return tb_demands.AsEnumerable();
        }

        // GET api/ApiProductDemand/5
        public tb_Demands Gettb_Demands(long id)
        {
            tb_Demands tb_demands = db.tb_Demands.Find(id);
            if (tb_demands == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_demands;
        }

        // PUT api/ApiProductDemand/5
        public HttpResponseMessage Puttb_Demands(long id, tb_Demands tb_demands)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_demands.DemandID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_demands).State = EntityState.Modified;

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

        // POST api/ApiProductDemand
        public HttpResponseMessage Posttb_Demands(tb_Demands tb_demands)
        {
            if (ModelState.IsValid)
            {
                db.tb_Demands.Add(tb_demands);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_demands);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_demands.DemandID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiProductDemand/5
        public HttpResponseMessage Deletetb_Demands(long id)
        {
            tb_Demands tb_demands = db.tb_Demands.Find(id);
            if (tb_demands == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_Demands.Remove(tb_demands);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_demands);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}