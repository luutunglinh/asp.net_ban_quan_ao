using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanQuanAo.Startup))]
namespace WebBanQuanAo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
