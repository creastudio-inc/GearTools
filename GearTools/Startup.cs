using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GearTools.Startup))]
namespace GearTools
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
