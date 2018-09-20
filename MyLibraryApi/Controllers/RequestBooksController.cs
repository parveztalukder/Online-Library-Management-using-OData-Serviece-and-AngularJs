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
    builder.EntitySet<RequestBook>("RequestBooks");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

      [EnableCors("*", "*", "*", "*")]
      
    public class RequestBooksController : ODataController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

           //[Authorize(Roles = "Admin,Member")]
        // GET: odata/RequestBooks
        [EnableQuery]
        public IQueryable<RequestBook> GetRequestBooks()
        {
            return db.RequestBooks;
        }
           [Authorize(Roles = "Admin,Member")]
        // GET: odata/RequestBooks(5)
        [EnableQuery]
        public SingleResult<RequestBook> GetRequestBook([FromODataUri] int key)
        {
            return SingleResult.Create(db.RequestBooks.Where(requestBook => requestBook.Id == key));
        }

        // PUT: odata/RequestBooks(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<RequestBook> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RequestBook requestBook = db.RequestBooks.Find(key);
            if (requestBook == null)
            {
                return NotFound();
            }

            patch.Put(requestBook);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestBookExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(requestBook);
        }

        // POST: odata/RequestBooks
        public IHttpActionResult Post(RequestBook requestBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestBooks.Add(requestBook);
            db.SaveChanges();

            return Created(requestBook);
        }

        // PATCH: odata/RequestBooks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<RequestBook> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RequestBook requestBook = db.RequestBooks.Find(key);
            if (requestBook == null)
            {
                return NotFound();
            }

            patch.Patch(requestBook);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestBookExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(requestBook);
        }

        // DELETE: odata/RequestBooks(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            RequestBook requestBook = db.RequestBooks.Find(key);
            if (requestBook == null)
            {
                return NotFound();
            }

            db.RequestBooks.Remove(requestBook);
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

        private bool RequestBookExists(int key)
        {
            return db.RequestBooks.Count(e => e.Id == key) > 0;
        }
    }
}
