using MyLibraryApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyLibraryApi.Controllers
{
       [EnableCors("*", "*", "*", "*")]
    public class AspNetUsersController : ApiController
    {
        private ApplicationDbContext Context = new ApplicationDbContext();

        public IHttpActionResult Get()
        {
            var retval = Context.Users.Select(x => new { Id = x.Id, Email = x.Email, PhoneNumber = x.PhoneNumber, Address = x.Address, Name=x.Name });

            return Ok(retval);
        }

        public IHttpActionResult GetbyEmail(string Email)
        {
            var retval = Context.Users.Where(u=>u.Email.Equals(Email)).Select(x => new { Id = x.Id, Email = x.Email, PhoneNumber = x.PhoneNumber, Address = x.Address, Name = x.Name }).SingleOrDefault();
            
            return Ok(retval);
        }


        public async Task<IHttpActionResult> Delete(string Id)
        {
            var userStore = new UserStore<ApplicationUser>(Context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user = Context.Users.Find(Id);

            var result = await userManager.DeleteAsync(user);

            return Ok();
        }
    }
}
