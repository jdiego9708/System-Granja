using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SISGranja.Startup))]
namespace SISGranja
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
