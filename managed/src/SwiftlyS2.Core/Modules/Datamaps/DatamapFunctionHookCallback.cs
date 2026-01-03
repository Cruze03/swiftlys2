using SwiftlyS2.Shared.Datamaps;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Datamaps;

internal class DatamapFunctionHookContext<T> : IDatamapFunctionHookContext<T> where T : ISchemaClass<T>
{
    public required T DatamapObject { get; internal set; }
    public HookResult HookResult { get; set; }
}