using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(prospekt.tel.Startup))]
namespace prospekt.tel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
