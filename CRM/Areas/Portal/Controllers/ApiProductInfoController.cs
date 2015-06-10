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
    public class ApiProductInfoController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiProductInfo
        public IEnumerable<tb_Products> Gettb_Products()
        {
            var tb_products = db.tb_Products.Include(t => t.tb_Suppliers);
            return tb_products.AsEnumerable();
        }

        // GET api/ApiProductInfo/5
        public tb_Products Gettb_Products(long id)
        {
            tb_Products tb_products = db.tb_Products.Find(id);
            if (tb_products == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_products;
        }

        // PUT api/ApiProductInfo/5
        public HttpResponseMessage Puttb_Products(long id, tb_Products tb_products)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_products.ProductID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_products).State = EntityState.Modified;

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

        // POST api/ApiProductInfo
        public HttpResponseMessage Posttb_Products(tb_Products tb_products)
        {
            if (ModelState.IsValid)
            {
                db.tb_Products.Add(tb_products);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_products);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_products.ProductID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiProductInfo/5
        public HttpResponseMessage Deletetb_Products(long id)
        {
            tb_Products tb_products = db.tb_Products.Find(id);
            if (tb_products == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_Products.Remove(tb_products);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_products);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}