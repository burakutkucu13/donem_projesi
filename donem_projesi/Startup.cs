using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(donem_projesi.Startup))]
namespace donem_projesi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
