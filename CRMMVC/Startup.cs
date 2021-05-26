using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRMMVC.Startup))]
namespace CRMMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
