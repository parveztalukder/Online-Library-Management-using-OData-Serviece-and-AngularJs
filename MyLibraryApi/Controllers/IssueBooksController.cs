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
    builder.EntitySet<IssueBook>("IssueBooks");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*", "*")]
    //[Authorize(Roles = "Admin")]
    public class IssueBooksController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/IssueBooks
        [EnableQuery]
        public IQueryable<IssueBook> GetIssueBooks()
        {
            return db.IssueBooks;
        }

        // GET: odata/IssueBooks(5)
        [EnableQuery]
        public SingleResult<IssueBook> GetIssueBook([FromODataUri] int key)
        {
            return SingleResult.Create(db.IssueBooks.Where(issueBook => issueBook.Id == key));
        }

        // PUT: odata/IssueBooks(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<IssueBook> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IssueBook issueBook = db.IssueBooks.Find(key);
            if (issueBook == null)
            {
                return NotFound();
            }

            patch.Put(issueBook);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueBookExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(issueBook);
        }

        // POST: odata/IssueBooks
        public IHttpActionResult Post(IssueBook issueBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IssueBooks.Add(issueBook);
            db.SaveChanges();

            return Created(issueBook);
        }

        // PATCH: odata/IssueBooks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<IssueBook> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IssueBook issueBook = db.IssueBooks.Find(key);
            if (issueBook == null)
            {
                return NotFound();
            }

            patch.Patch(issueBook);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueBookExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(issueBook);
        }

        // DELETE: odata/IssueBooks(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            IssueBook issueBook = db.IssueBooks.Find(key);
            if (issueBook == null)
            {
                return NotFound();
            }

            db.IssueBooks.Remove(issueBook);
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

        private bool IssueBookExists(int key)
        {
            return db.IssueBooks.Count(e => e.Id == key) > 0;
        }
    }
}
