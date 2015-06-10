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
    public class ApiDepartController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiDepart
        public IEnumerable<tb_Departs> Gettb_Departs()
        {
            return db.tb_Departs.AsEnumerable();
        }

        // GET api/ApiDepart/5
        public tb_Departs Gettb_Departs(long id)
        {
            tb_Departs tb_departs = db.tb_Departs.Find(id);
            if (tb_departs == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_departs;
        }

        // PUT api/ApiDepart/5
        public HttpResponseMessage Puttb_Departs(long id, tb_Departs tb_departs)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_departs.DepartID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_departs).State = EntityState.Modified;

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

        // POST api/ApiDepart
        public HttpResponseMessage Posttb_Departs(tb_Departs tb_departs)
        {
            if (ModelState.IsValid)
            {
                db.tb_Departs.Add(tb_departs);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_departs);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_departs.DepartID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiDepart/5
        public HttpResponseMessage Deletetb_Departs(long id)
        {
            tb_Departs tb_departs = db.tb_Departs.Find(id);
            if (tb_departs == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_Departs.Remove(tb_departs);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_departs);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}