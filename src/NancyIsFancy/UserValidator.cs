using System.Collections.Generic;
using Nancy.Authentication.Basic;
using Nancy.Security;

namespace NancyIsFancy
{
    public class UserValidator : IUserValidator
    {
        public IUserIdentity Validate(string username, string password)
        {
            if (username == "demo" && password == "demo")
            {
                return new DemoUserIdentity();
            }
            return null;
        }

        private class DemoUserIdentity : IUserIdentity
        {
            public string UserName { get { return "demo"; } }
            public IEnumerable<string> Claims { get { return new [] {"demo"}; } }
        }
    }
}