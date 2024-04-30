using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarShopIdentityManagementMVC.Startup))]
namespace CarShopIdentityManagementMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
