Entity Framework Code-Based Configuration Notes:
http://msdn.microsoft.com/en-us/data/jj680699.aspx

Moving DbConfiguration

There are cases where it is not possible to place your DbConfiguration class in the same assembly as your DbContext class. For example, you may have two DbContext classes each in different assemblies. There are two options for handling this.

The first option is to use the config file to specify the DbConfiguration instance to use. To do this, set the codeConfigurationType attribute of the entityFramework section. For example:

<entityFramework codeConfigurationType="MyNamespace.MyDbConfiguration, MyAssembly">
    ...Your EF config...
</entityFramework>
The value of codeConfigurationType must be the assembly and namespace qualified name of your DbConfiguration class.

The second option is to place DbConfigurationTypeAttribute on your context class. For example:

[DbConfigurationType(typeof(MyDbConfiguration))]
public class MyContextContext : DbContext
{
}
The value passed to the attribute can either be your DbConfiguration type - as shown above - or the assembly and namespace qualified type name string. For example:

[DbConfigurationType("MyNamespace.MyDbConfiguration, MyAssembly")]
public class MyContextContext : DbContext
{
}
 

Setting DbConfiguration explicitly

There are some situations where configuration may be needed before any DbContext type has been used. Examples of this include:

Using DbModelBuilder to build a model without a context
Using some other framework/utility code that utilizes a DbContext where that context is used before your application context is used
In such situations EF is unable to discover the configuration automatically and you must instead do one of the following:

Set the DbConfiguration type in the config file, as described in the Moving DbConfiguration section above
Call the static DbConfiguration.SetConfiguration method during application startup
 

Overriding DbConfiguration

There are some situations where you need to override the configuration set in the DbConfiguration. This is not typically done by application developers but rather by thrid party providers and plug-ins that cannot use a derived DbConfiguration class.

For this, EntityFramework allows an event handler to be registered that can modify existing configuration just before it is locked down.  It also provides a sugar method specifically for replacing any service returned by the EF service locator. This is how it is intended to be used:

At app startup (before EF is used) the plug-in or provider should register the event handler method for this event. (Note that this must happen before the application uses EF.)
The event handler makes a call to ReplaceService for every service that needs to be replaced.
For example, to repalce IDbConnectionFactory and DbProviderService you would register a handler something like this:

DbConfiguration.Loaded += (_, a) =>
   {
       a.ReplaceService<DbProviderServices>((s, k) => new MyProviderServices(s));
       a.ReplaceService<IDbConnectionFactory>((s, k) => new MyConnectionFactory(s));
   };
In the code above MyProviderServices and MyConnectionFactory represent your implementations of the service.

You can also add additional dependency handlers to get the same effect.

Note that you could also wrap DbProviderFactory in this way, but doing so will only effect EF and not uses of the DbProviderFactory outside of EF. For this reason you’ll probably want to continue to wrap DbProviderFactory as you have before.

You should also keep in mind the services that you run externally to your application - e.g. running migrations from Package Manager console. When you run migrate from the console it will attempt to find your DbConfiguration. However, whether or not it will get the wrapped service depends on where the event handler it registered. If it is registered as part of the construction of your DbConfiguration then the code should execute and the service should get wrapped. Usually this won’t be the case and this means that tooling won’t get the wrapped service