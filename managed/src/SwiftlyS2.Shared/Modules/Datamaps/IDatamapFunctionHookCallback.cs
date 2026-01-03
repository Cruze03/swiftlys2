using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Datamaps;

public interface IDatamapFunctionHookContext<T> where T : ISchemaClass<T>
{

    public T DatamapObject { get; }

    public HookResult HookResult { get; set;  }

}