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
    builder.EntitySet<Fine>("Fines");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*", "*")]
    //[Authorize(Roles = "Admin")]
    public class FinesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Fines
        [EnableQuery]
        public IQueryable<Fine> GetFines()
        {
            return db.Fines;
        }

        // GET: odata/Fines(5)
        [EnableQuery]
        public SingleResult<Fine> GetFine([FromODataUri] int key)
        {
            return SingleResult.Create(db.Fines.Where(fine => fine.Id == key));
        }

        // PUT: odata/Fines(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Fine> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Fine fine = db.Fines.Find(key);
            if (fine == null)
            {
                return NotFound();
            }

            patch.Put(fine);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FineExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fine);
        }

        // POST: odata/Fines
        public IHttpActionResult Post(Fine fine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fines.Add(fine);
            db.SaveChanges();

            return Created(fine);
        }

        // PATCH: odata/Fines(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Fine> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Fine fine = db.Fines.Find(key);
            if (fine == null)
            {
                return NotFound();
            }

            patch.Patch(fine);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FineExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(fine);
        }

        // DELETE: odata/Fines(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Fine fine = db.Fines.Find(key);
            if (fine == null)
            {
                return NotFound();
            }

            db.Fines.Remove(fine);
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

        private bool FineExists(int key)
        {
            return db.Fines.Count(e => e.Id == key) > 0;
        }
    }
}
