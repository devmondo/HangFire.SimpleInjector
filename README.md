HangFire.SimpleInjector
=======================

HangFire Simple Injector Integration


To use this, head to the link below and install the Nuget Package. 

https://www.nuget.org/packages/HangFire.SimpleInjector

The package contains all the dependencies, and you can always download the source if you want :)

To configure Hangfire to use SimpleInjector, configure your container and call the `IGlobalConfiguration` extension method, `UseSimpleInjectorActivator`:

```csharp

var container = new Container();
//configuration

GlobalConfiguration.Configuration.UseSimpleInjectorActivator(container);
```