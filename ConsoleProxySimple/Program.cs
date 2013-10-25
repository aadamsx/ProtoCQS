using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//A proxy is an object that can be used to control creation and access of a more complex object thereby deferring the cost of 
//creating it until the time its needed.

//Below is a simple implementation of the proxy pattern in C#. The ComplexProtectedExpensiveResource is private to the 
//ProxyContainer and cannot be instantiated by a client. The client creates an instance of the SimpleProxy class which controls 
//its access to the more complex and expensive to create ComplexProtectedExpensiveResource class.

//Note that the ComplexProtectedExpensiveResource is created by the SimpleProxy instance only when needed and after 
//verifying that the client indeed has access to it.

//Proxy Design Pattern, lazy loading using Proxy Design Pattern

//What is Proxy Design Pattern 

//1. Proxy design patten works on the principal of exposing an Instance through a proxy instead of actual object. 

//2. Client would never know anything about actual object and through Proxy only relevant behavior of actual object will be exposed to client.

//3. Proxy Pattern can be used for applying security on actual Object. Service provider does not want actual class to be visible to any client Instead It would be shared as per Client contract agreement . Service provider may agree to share only a part of Service with it's client and for that It may expose a different contract in the form of interface in java . 

//4. This concept is very useful for lazily loading an instance . Data will be loaded only when it is actually required in an operation . 

namespace ConsoleProxySimple
{
    // The Client
    class ProxyPattern : ProxyContainer
    {
        static void Main(string[] args)
        {
            var simpleProxy = new SimpleProxy("password");
            simpleProxy.DoWork();
        }
    }

    public class ProxyContainer
    {
        private class ComplexProtectedExpensiveResource
        {
            internal void DoWork()
            {
                //do some heavy lifting
            }
        }

        // The Proxy
        public class SimpleProxy
        {
            ComplexProtectedExpensiveResource _complexProtectedResource;
            private string _password;

            public SimpleProxy(string password)
            {
                _password = password;
            }

            public void DoWork()
            {
                if (Authenticate())
                {
                    _complexProtectedResource.DoWork();    
                }
            }

            bool Authenticate()
            {
                //authenticate request
                if (_password == "password")
                {
                    //create expensive object if authenticated
                    if (_complexProtectedResource == null)
                        _complexProtectedResource = new ComplexProtectedExpensiveResource();
                    return true;
                }
                return false;
            }
        }
    }
}
