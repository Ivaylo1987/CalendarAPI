using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalendarAPI.Web.Startup))]
namespace CalendarAPI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
