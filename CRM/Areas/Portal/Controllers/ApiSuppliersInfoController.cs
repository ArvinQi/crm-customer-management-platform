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
    public class ApiSuppliersInfoController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiSuppliersInfo
        public IEnumerable<tb_Suppliers> Gettb_Suppliers()
        {
            return db.tb_Suppliers.AsEnumerable();
        }

        // GET api/ApiSuppliersInfo/5
        public tb_Suppliers Gettb_Suppliers(long id)
        {
            tb_Suppliers tb_suppliers = db.tb_Suppliers.Find(id);
            if (tb_suppliers == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_suppliers;
        }

        // PUT api/ApiSuppliersInfo/5
        public HttpResponseMessage Puttb_Suppliers(long id, tb_Suppliers tb_suppliers)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_suppliers.SupplierID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_suppliers).State = EntityState.Modified;

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

        // POST api/ApiSuppliersInfo
        public HttpResponseMessage Posttb_Suppliers(tb_Suppliers tb_suppliers)
        {
            if (ModelState.IsValid)
            {
                db.tb_Suppliers.Add(tb_suppliers);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_suppliers);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_suppliers.SupplierID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiSuppliersInfo/5
        public HttpResponseMessage Deletetb_Suppliers(long id)
        {
            tb_Suppliers tb_suppliers = db.tb_Suppliers.Find(id);
            if (tb_suppliers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_Suppliers.Remove(tb_suppliers);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_suppliers);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}