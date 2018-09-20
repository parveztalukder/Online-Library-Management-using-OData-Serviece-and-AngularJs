using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using MyLibraryApi.Models;
using MyLibraryApi.API.Models;

namespace MyLibraryApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.EnableCors();
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books");
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Rack>("Racks");
            builder.EntitySet<Member>("Members");
            builder.EntitySet<Purchase>("Purchases");
            builder.EntitySet<IssueBook>("IssueBooks");
            builder.EntitySet<IssueBookHistory>("IssueBookHistories");
            builder.EntitySet<Fine>("Fines");
            builder.EntitySet<RequestBook>("RequestBooks");
            builder.EntitySet<Finehis>("Finehis");
            builder.EntitySet<Message>("Messages");
            builder.EntitySet<Subscriber>("Subscribers");
            builder.EntitySet<SBook>("SBooks");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<OrderDetail>("OrderDetails");
            builder.EntitySet<StockIn>("StockIns");


            var a = builder.Entity<Book>().Collection.Action("GetBooksbyCategory");
            a.Parameter<int>("catid");

            builder.Entity<Book>().Collection.Action("GetBookall").ReturnsCollection<BookVM>();




            //builder.Entity<IssueBookReq>().Collection.Action("GetIssueReq").ReturnsCollection<IssueBookReqVM>();

            //config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
            //var confirmBook = builder.Entity<IssueBookReq>().Collection.Action("GetNotifications").Returns<bool>();
            //confirmBook.CollectionParameter<IssueBookReqVM>("issue");


            var memb = builder.Entity<Member>().Action("login");

            //var bkissue = builder.Entity<Book>().Action("UpdateIssueNo");
            var Confirmbook = builder.Entity<Book>().Collection.Action("UpdateIssueNo")
                                        .Returns<string>();
           

            //Action
            builder.Entity<SBook>()
                .Collection
                .Action("AcSBooks")
                .ReturnsCollection<SBookVM>();


            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());


        }
    }
}
