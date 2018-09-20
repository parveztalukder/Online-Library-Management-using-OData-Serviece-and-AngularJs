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
    builder.EntitySet<Finehis>("Finehis");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
     [EnableCors("*", "*", "*", "*")]
     //[Authorize(Roles = "Admin")]
    public class FinehisController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Finehis
        [EnableQuery]
        public IQueryable<Finehis> GetFinehis()
        {
            return db.Finehis;
        }

        // GET: odata/Finehis(5)
        [EnableQuery]
        public SingleResult<Finehis> GetFinehis([FromODataUri] int key)
        {
            return SingleResult.Create(db.Finehis.Where(finehis => finehis.Id == key));
        }

        // PUT: odata/Finehis(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Finehis> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Finehis finehis = db.Finehis.Find(key);
            if (finehis == null)
            {
                return NotFound();
            }

            patch.Put(finehis);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinehisExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(finehis);
        }

        // POST: odata/Finehis
        public IHttpActionResult Post(Finehis finehis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Finehis.Add(finehis);
            db.SaveChanges();

            return Created(finehis);
        }

        // PATCH: odata/Finehis(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Finehis> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Finehis finehis = db.Finehis.Find(key);
            if (finehis == null)
            {
                return NotFound();
            }

            patch.Patch(finehis);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinehisExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(finehis);
        }

        // DELETE: odata/Finehis(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Finehis finehis = db.Finehis.Find(key);
            if (finehis == null)
            {
                return NotFound();
            }

            db.Finehis.Remove(finehis);
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

        private bool FinehisExists(int key)
        {
            return db.Finehis.Count(e => e.Id == key) > 0;
        }
    }
}
