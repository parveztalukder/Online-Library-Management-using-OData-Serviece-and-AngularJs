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
    builder.EntitySet<Rack>("Racks");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*", "*")]
    //[Authorize(Roles = "Admin")]
    public class RacksController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Racks
        [EnableQuery]
        public IQueryable<Rack> GetRacks()
        {
            return db.Rack;
        }

        // GET: odata/Racks(5)
        [EnableQuery]
        public SingleResult<Rack> GetRack([FromODataUri] int key)
        {
            return SingleResult.Create(db.Rack.Where(rack => rack.Id == key));
        }

        // PUT: odata/Racks(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Rack> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rack rack = db.Rack.Find(key);
            if (rack == null)
            {
                return NotFound();
            }

            patch.Put(rack);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RackExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(rack);
        }

        // POST: odata/Racks
        public IHttpActionResult Post(Rack rack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rack.Add(rack);
            db.SaveChanges();

            return Created(rack);
        }

        // PATCH: odata/Racks(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Rack> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rack rack = db.Rack.Find(key);
            if (rack == null)
            {
                return NotFound();
            }

            patch.Patch(rack);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RackExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(rack);
        }

        // DELETE: odata/Racks(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Rack rack = db.Rack.Find(key);
            if (rack == null)
            {
                return NotFound();
            }

            db.Rack.Remove(rack);
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

        private bool RackExists(int key)
        {
            return db.Rack.Count(e => e.Id == key) > 0;
        }
    }
}
