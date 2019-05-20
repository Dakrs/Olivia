using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Olivia.Startup))]
namespace Olivia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
