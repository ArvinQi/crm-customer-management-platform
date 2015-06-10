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
    public class ApiClientsController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiClients
        public IEnumerable<tb_Clients> Gettb_Clients()
        {
            var tb_clients = db.tb_Clients.Include(t => t.tb_Users);
            return tb_clients.AsEnumerable();
        }

        // GET api/ApiClients/5
        public tb_Clients Gettb_Clients(long id)
        {
            tb_Clients tb_clients = db.tb_Clients.Find(id);
            if (tb_clients == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_clients;
        }

        // PUT api/ApiClients/5
        public HttpResponseMessage Puttb_Clients(long id, tb_Clients tb_clients)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_clients.ClientID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_clients).State = EntityState.Modified;

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

        // POST api/ApiClients
        public HttpResponseMessage Posttb_Clients(tb_Clients tb_clients)
        {
            if (ModelState.IsValid)
            {
                db.tb_Clients.Add(tb_clients);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_clients);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_clients.ClientID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiClients/5
        public HttpResponseMessage Deletetb_Clients(long id)
        {
            tb_Clients tb_clients = db.tb_Clients.Find(id);
            if (tb_clients == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_Clients.Remove(tb_clients);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_clients);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}