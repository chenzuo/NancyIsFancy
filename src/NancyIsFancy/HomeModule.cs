using Nancy;

namespace NancyIsFancy
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>  "Nancy is Fancy!";
        }
    }
}