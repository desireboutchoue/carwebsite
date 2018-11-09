using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(carwebsite.Startup))]
namespace carwebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
