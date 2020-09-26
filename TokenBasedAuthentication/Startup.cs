using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TokenBasedAuthentication.Startup))]
namespace TokenBasedAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
