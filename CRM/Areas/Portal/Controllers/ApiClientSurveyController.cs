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
    public class ApiClientSurveyController : ApiController
    {
        private CRMEntities db = new CRMEntities();

        // GET api/ApiClientSurvey
        public IEnumerable<tb_ClientSurvey> Gettb_ClientSurvey()
        {
            var tb_clientsurvey = db.tb_ClientSurvey.Include(t => t.tb_Clients).Include(t => t.tb_Products).Include(t => t.tb_Users);
            return tb_clientsurvey.AsEnumerable();
        }

        // GET api/ApiClientSurvey/5
        public tb_ClientSurvey Gettb_ClientSurvey(long id)
        {
            tb_ClientSurvey tb_clientsurvey = db.tb_ClientSurvey.Find(id);
            if (tb_clientsurvey == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tb_clientsurvey;
        }

        // PUT api/ApiClientSurvey/5
        public HttpResponseMessage Puttb_ClientSurvey(long id, tb_ClientSurvey tb_clientsurvey)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tb_clientsurvey.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(tb_clientsurvey).State = EntityState.Modified;

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

        // POST api/ApiClientSurvey
        public HttpResponseMessage Posttb_ClientSurvey(tb_ClientSurvey tb_clientsurvey)
        {
            if (ModelState.IsValid)
            {
                db.tb_ClientSurvey.Add(tb_clientsurvey);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tb_clientsurvey);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tb_clientsurvey.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ApiClientSurvey/5
        public HttpResponseMessage Deletetb_ClientSurvey(long id)
        {
            tb_ClientSurvey tb_clientsurvey = db.tb_ClientSurvey.Find(id);
            if (tb_clientsurvey == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.tb_ClientSurvey.Remove(tb_clientsurvey);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tb_clientsurvey);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}