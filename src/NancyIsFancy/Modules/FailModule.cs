using System;
using Nancy;

namespace NancyIsFancy.Modules
{
    public class FailModule : NancyModule
    {
        public FailModule()
        {
            Get["/blowup"] = _ =>
                {
                    throw new Exception("fail!");
                };
        }
    }
}