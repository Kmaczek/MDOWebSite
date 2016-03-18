using Owin;

namespace Mdo.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseStaticFiles("/mdo_images");
            app.UseNancy();
        }
    }
}
