using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebQueueTest.Startup))]
namespace WebQueueTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
