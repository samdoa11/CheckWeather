using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteAnzeige.Startup))]
namespace WebsiteAnzeige
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
