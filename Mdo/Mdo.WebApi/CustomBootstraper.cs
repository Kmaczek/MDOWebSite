using System;
using System.Configuration;
using Mdo.Persistence;
using Mdo.Persistence.Interfaces;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using PersistenceMocks;

namespace Mdo.WebApi
{
    public class CustomBootstraper: DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:22222");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS");
                ctx.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Accept,Origin,Content-type");
                ctx.Response.Headers.Add("Access-Control-Expose-Headers", "Accept,Origin,Content-type,Status-Code");
            });

            bool isTest;
            Boolean.TryParse(ConfigurationManager.AppSettings.Get("test"), out isTest);

            if (!isTest)
            {
                RegisterDependencies(container);
            }
            else
            {
                RegisterTestDependencies(container);
            }

            base.ApplicationStartup(container, pipelines);
        }

        private static void RegisterDependencies(TinyIoCContainer container)
        {
            container.Register<IUserRepository, UserRepository>();
        }

        private static void RegisterTestDependencies(TinyIoCContainer container)
        {
            container.Register<IUserRepository, UserRepositoryMock>();
        }
    }
}
