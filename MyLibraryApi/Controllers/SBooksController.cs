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
using MyLibraryApi.API.Models;
using MyLibraryApi.Models;
using System.Web.Http.Cors;

namespace MyLibraryApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using MyLibraryApi.API.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<SBook>("SBooks");
    builder.EntitySet<StockIn>("StockIns"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*")]
    public class SBooksController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Products
        [EnableQuery]
        public IQueryable<SBook> GetSBooks()
        {
            return db.SBooks;
        }

        // GET: odata/Products(5)
        [EnableQuery]
        public SingleResult<SBook> GetSBook([FromODataUri] int key)
        {
            return SingleResult.Create(db.SBooks.Where(sbook => sbook.Id == key));
        }

        // PUT: odata/Products(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<SBook> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SBook sbook = db.SBooks.Find(key);
            if (sbook == null)
            {
                return NotFound();
            }

            patch.Put(sbook);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(sbook);
        }

        // POST: odata/Products
        public IHttpActionResult Post(SBook sbook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SBooks.Add(sbook);
            db.SaveChanges();

            return Created(sbook);
        }

        // PATCH: odata/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<SBook> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SBook sbook = db.SBooks.Find(key);
            if (sbook == null)
            {
                return NotFound();
            }

            patch.Patch(sbook);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(sbook);
        }

        // DELETE: odata/Products(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            SBook sbook = db.SBooks.Find(key);
            if (sbook == null)
            {
                return NotFound();
            }

            db.SBooks.Remove(sbook);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Products(5)/StockIns
        [EnableQuery]
        public IQueryable<StockIn> GetStockIns([FromODataUri] int key)
        {
            return db.SBooks.Where(m => m.Id == key).SelectMany(m => m.StockIns);
        }
        [HttpPost]
        public IQueryable<SBookVM> AcSBooks()
        {
            return db.SBooks.Select(p => new SBookVM
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Description = p.Description,
                Picture = p.Picture,
                Stocklevel = p.Stocklevel,
                BriefDescription = p.Description.Length > 20 ? p.Description.Substring(0, 20) + "..." : p.Description
            });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int key)
        {
            return db.SBooks.Count(e => e.Id == key) > 0;
        }
    }
}
