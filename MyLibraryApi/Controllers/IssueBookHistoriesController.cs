using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using MyLibraryApi.Models;
using System.Web.Http.Cors;

namespace MyLibraryApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Library_Management_API.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<IssueBookHistory>("IssueBookHistories");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*", "*")]
    //[Authorize(Roles = "Admin")]
    public class IssueBookHistoriesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/IssueBookHistories
        [EnableQuery]
        public IQueryable<IssueBookHistory> GetIssueBookHistories()
        {
            return db.IssueBookHistories;
        }

        // GET: odata/IssueBookHistories(5)
        [EnableQuery]
        public SingleResult<IssueBookHistory> GetIssueBookHistory([FromODataUri] int key)
        {
            return SingleResult.Create(db.IssueBookHistories.Where(issueBookHistory => issueBookHistory.Id == key));
        }

        // PUT: odata/IssueBookHistories(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<IssueBookHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IssueBookHistory issueBookHistory = db.IssueBookHistories.Find(key);
            if (issueBookHistory == null)
            {
                return NotFound();
            }

            patch.Put(issueBookHistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueBookHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(issueBookHistory);
        }

        // POST: odata/IssueBookHistories
        public IHttpActionResult Post(IssueBookHistory issueBookHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IssueBookHistories.Add(issueBookHistory);
            db.SaveChanges();

            return Created(issueBookHistory);
        }

        // PATCH: odata/IssueBookHistories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<IssueBookHistory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IssueBookHistory issueBookHistory = db.IssueBookHistories.Find(key);
            if (issueBookHistory == null)
            {
                return NotFound();
            }

            patch.Patch(issueBookHistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueBookHistoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(issueBookHistory);
        }

        // DELETE: odata/IssueBookHistories(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            IssueBookHistory issueBookHistory = db.IssueBookHistories.Find(key);
            if (issueBookHistory == null)
            {
                return NotFound();
            }

            db.IssueBookHistories.Remove(issueBookHistory);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IssueBookHistoryExists(int key)
        {
            return db.IssueBookHistories.Count(e => e.Id == key) > 0;
        }
    }
}
