using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ColorBlinder.Startup))]
namespace ColorBlinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
