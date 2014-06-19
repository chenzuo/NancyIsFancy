using Nancy;

namespace NancyIsFancy.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
                {
                    return Negotiate
                        .WithModel("Nancy is Fancy!")
                        .WithView("index");
                };
        }
    }
}