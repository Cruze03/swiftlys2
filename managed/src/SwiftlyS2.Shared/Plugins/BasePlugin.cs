using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.Plugins;

public abstract class BasePlugin : IPlugin
{

  protected ISwiftlyCore Core { get; private init; }

  public BasePlugin( ISwiftlyCore core )
  {

    Core = core;

    AppDomain.CurrentDomain.UnhandledException += ( sender, e ) =>
    {
      var trace = new StackTrace(true);
      Core.Logger.LogCritical(e.ExceptionObject as Exception, "CRITICAL: Unhandled exception in plugin. Aborting.");
      Core.Logger.LogCritical("Stack trace:" + string.Join("", trace.GetFrames().Select(f => $"\n  at {f.GetMethod()} in {f.GetFileName()}:line {f.GetFileLineNumber()}")));
    };

    TaskScheduler.UnobservedTaskException += ( sender, e ) =>
    {
      Core.Logger.LogCritical(e.Exception, "CRITICAL: Unobserved task exception in plugin. Aborting.");
      e.SetObserved();
    };

    Console.SetOut(new ConsoleRedirector());
    Console.SetError(new ConsoleRedirector());
  }

  public virtual void ConfigureSharedInterface( IInterfaceManager interfaceManager ) { }

  public virtual void UseSharedInterface( IInterfaceManager interfaceManager ) { }

  public virtual void OnSharedInterfaceInjected( IInterfaceManager interfaceManager ) { }

  public virtual void OnAllPluginsLoaded() { }

  public abstract void Load( bool hotReload );

  public abstract void Unload();
}