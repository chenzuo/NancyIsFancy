using Nancy;
using Nancy.Security;

namespace NancyIsFancy.Modules
{
    public class SecureModule : NancyModule
    {
        public SecureModule()
            : base("/secure")
        {
            this.RequiresAuthentication();
            this.RequiresClaims(new[] { "demo" });

            Get["/"] = _ => "You've been granted access!";
        }
    }
}