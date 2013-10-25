using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Security;
using Data;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;

namespace ProtoConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new Container();
            PrototypeBoostrapper.Bootstrap(container);

            IRepository<Entity> repository = 
                new ValidateUserDecorator<Entity>(
                    new LoggingDecorator<Entity>(
                        new Repository<Entity>(
                            new PrototypeContext()), 
                        new ConsoleLogger()), 
                    new ClaimsPrincipal());

            var controller = new Controller(repository);

            var e = new Entity
            {
                Id = 1,
                Name = "Example Entity",
                Description = "Used by Decorators",
                RowGuild = Guid.NewGuid()
            };

            controller.Create(e);
        }
    }
}
