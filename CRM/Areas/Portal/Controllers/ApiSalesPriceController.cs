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
    public class ApiSalesPriceController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/SalesPrice
        public IEnumerable<tb_SalesPrice> Gettb_SalesPrice()
        {
            var tb_salesprice = db.tb_SalesPrice.Include(t => t.tb_Clients).Include(t => t.tb_Products).Include(t => t.tb_Users);
            return tb_salesprice.AsEnumerable();
        }

        // GET api/SalesPrice/5
        public tb_SalesPrice Gettb_SalesPrice(long id)
        {
            tb_SalesPrice tb_salesprice = db.tb_SalesPrice.Find(id);
            if (tb_salesprice == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_salesprice;
        }

        // PUT api/SalesPrice/5
        public HttpResponseMessage Puttb_SalesPrice(long id, tb_SalesPrice tb_salesprice)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_salesprice.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_salesprice).State = EntityState.Modified;

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

        // POST api/SalesPrice
        public HttpResponseMessage Posttb_SalesPrice(tb_SalesPrice tb_salesprice)
        {
            if (ModelState.IsValid)
            {
                db.tb_SalesPrice.Add(tb_salesprice);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_salesprice);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_salesprice.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SalesPrice/5
        public HttpResponseMessage Deletetb_SalesPrice(long id)
        {
            tb_SalesPrice tb_salesprice = db.tb_SalesPrice.Find(id);
            if (tb_salesprice == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_SalesPrice.Remove(tb_salesprice);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_salesprice);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}