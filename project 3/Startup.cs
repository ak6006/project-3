using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(project_3.Startup))]
namespace project_3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                ExpireTimeSpan = TimeSpan.FromDays(1),
                LoginPath = new PathString("~/Account/Login"),
            }) ;
        }
    }
}
