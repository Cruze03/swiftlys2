using SwiftlyS2.Shared.Datamaps;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Datamaps;

public interface IDatamapFunctionOperator<T> where T : ISchemaClass<T>
{
    
    public void HookPre(Action<IDatamapFunctionHookContext<T>> callback);

    public void HookPost(Action<IDatamapFunctionHookContext<T>> callback);

    public void Invoke(T schemaObject);

    public void InvokeOriginal(T schemaObject);

}