using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Main_Web_Application.Startup))]
namespace Main_Web_Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
