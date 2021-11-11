using System;

namespace LootCouncil.Utility.Exceptions
{
    public class ServerIsTeapotException : Exception
    {
        public ServerIsTeapotException():base("I'm a teapot")
        {
            
        }
    }
}