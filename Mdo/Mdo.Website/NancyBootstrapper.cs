using Nancy;
using Nancy.Conventions;

namespace Mdo.Website
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder
                .AddDirectory("app", @"app"));
        }
    }
}
