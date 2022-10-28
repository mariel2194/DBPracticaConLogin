using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DBPracticaConLogin.Startup))]
namespace DBPracticaConLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
