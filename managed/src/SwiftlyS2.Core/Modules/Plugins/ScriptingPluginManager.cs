using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace SwiftlyS2.Core.Plugins;

internal class ScriptingPluginManager : IPluginManager
{
    private readonly PluginManager manager;

    public ScriptingPluginManager( PluginManager manager )
    {
        this.manager = manager;
    }

    public Dictionary<string, PluginMetadata> GetAllPluginMetadata()
    {
        var plugins = manager.GetPlugins();
        var result = new Dictionary<string, PluginMetadata>();
        foreach (var plugin in plugins)
        {
            if (plugin.Metadata != null && plugin.Metadata.Id != null)
            {
                result[plugin.Metadata.Id] = plugin.Metadata;
            }
        }
        return result;
    }

    public IEnumerable<string> GetAllPlugins()
    {
        var plugins = manager.GetPlugins();
        foreach (var plugin in plugins)
        {
            if (plugin.Metadata != null && plugin.Metadata.Id != null)
            {
                yield return plugin.Metadata.Id;
            }
        }
    }

    public Dictionary<string, PluginStatus> GetAllPluginStatuses()
    {
        var plugins = manager.GetPlugins();
        var result = new Dictionary<string, PluginStatus>();
        foreach (var plugin in plugins)
        {
            if (plugin.Metadata != null && plugin.Metadata.Id != null && plugin.Status != null)
            {
                result[plugin.Metadata.Id] = plugin.Status.Value;
            }
        }
        return result;
    }

    public PluginMetadata? GetPluginMetadata( string pluginId )
    {
        var plugins = manager.GetPlugins();
        foreach (var plugin in plugins)
        {
            if (plugin.Metadata != null && plugin.Metadata.Id == pluginId)
            {
                return plugin.Metadata;
            }
        }
        return null;
    }

    public string? GetPluginPath( string pluginId )
    {
        var plugins = manager.GetPlugins();
        foreach (var plugin in plugins)
        {
            if (plugin.Metadata != null && plugin.Metadata.Id == pluginId)
            {
                return plugin.PluginDirectory;
            }
        }
        return null;
    }

    public Dictionary<string, string> GetPluginPaths()
    {
        var plugins = manager.GetPlugins();
        var result = new Dictionary<string, string>();
        foreach (var plugin in plugins)
        {
            if (plugin.Metadata != null && plugin.Metadata.Id != null && plugin.PluginDirectory != null)
            {
                result[plugin.Metadata.Id] = plugin.PluginDirectory;
            }
        }
        return result;
    }

    public PluginStatus? GetPluginStatus( string pluginId )
    {
        var plugins = manager.GetPlugins();
        foreach (var plugin in plugins)
        {
            if (plugin.Metadata != null && plugin.Metadata.Id == pluginId)
            {
                return plugin.Status;
            }
        }
        return null;
    }

    public bool LoadPlugin( string pluginId, bool silent )
    {
        return manager.LoadPluginById(pluginId, silent);
    }

    public bool ReloadPlugin( string pluginId, bool silent )
    {
        return manager.ReloadPluginById(pluginId, silent);
    }

    public bool UnloadPlugin( string pluginId, bool silent )
    {
        return manager.UnloadPluginById(pluginId, silent);
    }
}