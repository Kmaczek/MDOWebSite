using Mdo.Website.NancyConfig;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace Mdo.Website
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.BeforeRequest += (ctx) =>
            {
                var path = ctx.Request.Path;
                return ctx.Response;
            };

            base.ApplicationStartup(container, pipelines);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("app", @"app"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("mdo_images", @"mdo_images"));
        }

#if DEBUG
        protected override IRootPathProvider RootPathProvider => new CustomRootPathProvider();
#endif
    }
}
