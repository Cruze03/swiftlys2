using System.Collections.Concurrent;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

internal static class EntitySystemManager
{
    public static ConcurrentDictionary<uint, CEntityInstance> ActiveEntities { get; } = [];

    public static void OnEntityCreated( CEntityInstance instance )
    {
        _ = ActiveEntities.TryAdd(instance.Index, instance);
    }

    public static void OnEntityRemoved( CEntityInstance instance )
    {
        _ = ActiveEntities.TryRemove(instance.Index, out _);
    }
}