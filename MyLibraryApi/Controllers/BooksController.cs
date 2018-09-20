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
    builder.EntitySet<Book>("Books");
    builder.EntitySet<Category>("Category"); 
    builder.EntitySet<Rack>("Rack"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors("*", "*", "*", "*")]
    public class BooksController : ODataController
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();
         //[Authorize(Roles = "Admin,Member")]
        // GET: odata/Books
        [EnableQuery]
        public IQueryable<Book> GetBooks()
        {
            return db.Book;
        }

         //[Authorize(Roles = "Admin,Member")]
        // GET: odata/Books(5)
        [EnableQuery]
        public SingleResult<Book> GetBook([FromODataUri] int key)
        {
            return SingleResult.Create(db.Book.Where(book => book.Id == key));
        }

        // PUT: odata/Books(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Book> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Book book = db.Book.Find(key);
            if (book == null)
            {
                return NotFound();
            }

            patch.Put(book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(book);
        }

        // POST: odata/Books
        public IHttpActionResult Post(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Book.Add(book);
            db.SaveChanges();

            return Created(book);
        }

        // PATCH: odata/Books(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Book> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Book book = db.Book.Find(key);
            if (book == null)
            {
                return NotFound();
            }

            patch.Patch(book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(book);
        }

        // DELETE: odata/Books(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Book book = db.Book.Find(key);
            if (book == null)
            {
                return NotFound();
            }

            db.Book.Remove(book);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Books(5)/Category
        [EnableQuery]
        public SingleResult<Category> GetCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Book.Where(m => m.Id == key).Select(m => m.Category));
        }

        // GET: odata/Books(5)/Rack
        [EnableQuery]
        public SingleResult<Rack> GetRack([FromODataUri] int key)
        {
            return SingleResult.Create(db.Book.Where(m => m.Id == key).Select(m => m.Rack));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int key)
        {
            return db.Book.Count(e => e.Id == key) > 0;
        }

        [HttpPost]
        public IQueryable<BookVM> GetBookall()
        {
            return db.Book.Include(c => c.Category).Include(r => r.Rack).Select(s => new BookVM
            {
                Id = s.Id,
                BookAuth = s.BookAuth,
                BookName = s.BookName,
                Rack = s.Rack.RackName,
                Category = s.Category.CategoryName,

                BookStatus = s.BookStatus,
                AvilableBook=s.AvilableBook,
                IssueBook=s.IssueBook



            });
        }
    }
}
