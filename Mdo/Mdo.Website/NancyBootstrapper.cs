using System.Runtime.CompilerServices;
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
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder
                .AddDirectory("app", @"app"));
        }
    }
}
