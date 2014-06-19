using System;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace NancyIsFancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.BeforeRequest += ctx =>
                {
                    Console.WriteLine("BEFORE - [{0}] {1}", ctx.Request.Method, ctx.Request.Url);

                    if (ctx.Request.Headers.Accept.Any(x => x.Item1 == "text/html"))
                    {
                        return null;
                    }

                    var superSecretHeader = ctx.Request.Headers["X-Super-Secret"].FirstOrDefault();
                    if (superSecretHeader != "NancyIsFancy!")
                    {
                        Console.WriteLine("l33t hax0rz caught attempting to infiltrate the system!");
                        return HttpStatusCode.Unauthorized;
                    }

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