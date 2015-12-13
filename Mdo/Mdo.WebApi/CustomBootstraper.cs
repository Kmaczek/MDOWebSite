using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Mdo.WebApi
{
    public class CustomBootstraper: DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:12345");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS");
                ctx.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Accept,Origin,Content-type");
                ctx.Response.Headers.Add("Access-Control-Expose-Headers", "Accept,Origin,Content-type,Status-Code");
            });

            base.ApplicationStartup(container, pipelines);
        }
    }
}
