using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vzah.Startup))]
namespace Vzah
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
