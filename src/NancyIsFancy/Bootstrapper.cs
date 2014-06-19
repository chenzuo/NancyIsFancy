using System;
using Nancy;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace NancyIsFancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            BasicAuthentication.Enable(pipelines, new BasicAuthenticationConfiguration(container.Resolve<IUserValidator>(), "realm"));
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.BeforeRequest += ctx =>
                {
                    Console.WriteLine("BEFORE - [{0}] {1}", ctx.Request.Method, ctx.Request.Url);

                    return null;
                };

            pipelines.AfterRequest += ctx =>
                {
                    Console.WriteLine("AFTER - {0}", ctx.Response.StatusCode);
                };

            pipelines.OnError += (ctx, ex) =>
                {
                    Console.WriteLine("ERROR - Oh Noes! Something went horribly wrong!");
                    Console.WriteLine(ex);

                    return HttpStatusCode.InternalServerError;
                };
        }
    }
}