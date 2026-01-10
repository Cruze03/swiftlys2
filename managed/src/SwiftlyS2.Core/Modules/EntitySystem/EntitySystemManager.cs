using System.Collections.Concurrent;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

internal static class EntitySystemManager
{
    public static ConcurrentDictionary<uint, CEntityInstance> ActiveEntities { get; } = [];

    public static CEntityInstance OnEntityCreated( nint instancePtr )
    {
        var entityInstance = new CEntityInstanceImpl(instancePtr);
        var actualInstance = ClassConvertor.ConvertEntityByDesignerName(instancePtr, entityInstance.DesignerName);

        _ = ActiveEntities.TryAdd(actualInstance.Index, actualInstance);

        return actualInstance;
    }

    public static CEntityInstance OnEntityRemoved( nint entityPtr, uint entityIndex )
    {
        return ActiveEntities.TryGetValue(entityIndex, out var val) ? val : new CEntityInstanceImpl(entityPtr);
    }
}