using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyLibraryClient.Startup))]
namespace MyLibraryClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
