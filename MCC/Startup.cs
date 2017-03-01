using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MCC.Startup))]
namespace MCC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
