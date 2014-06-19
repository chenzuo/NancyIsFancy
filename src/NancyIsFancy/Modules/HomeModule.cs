using Nancy;

namespace NancyIsFancy.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>  "Nancy is Fancy!";
        }
    }
}