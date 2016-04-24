using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BeyondThemes.BeyondAdmin.Startup))]
namespace BeyondThemes.BeyondAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
