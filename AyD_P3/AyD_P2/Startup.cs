using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AyD_P2.Startup))]
namespace AyD_P2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
