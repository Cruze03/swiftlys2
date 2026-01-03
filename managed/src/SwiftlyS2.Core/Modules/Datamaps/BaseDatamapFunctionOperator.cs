using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Datamaps;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Datamaps;

internal class BaseDatamapFunctionOperator<T> : IDatamapFunctionOperator<T>
    where T : ISchemaClass<T>
{

    private BaseDatamapFunction<T> _Owner { get; set; }

    public BaseDatamapFunctionOperator( BaseDatamapFunction<T> owner )
    {
        _Owner = owner;
    }

    private List<Action<IDatamapFunctionHookContext<T>>> _PreCallbacks = [];
    private List<Action<IDatamapFunctionHookContext<T>>> _PostCallbacks = [];

    internal bool CallbackPre( nint ptr )
    {

        var ctx = new DatamapFunctionHookContext<T>() {
            DatamapObject = Helper.AsSchema<T>(ptr),
            HookResult = HookResult.Continue
        };

        foreach (var callback in _PreCallbacks)
        {
            callback(ctx);
            if (ctx.HookResult == HookResult.Handled) return true;
            if (ctx.HookResult == HookResult.Stop) return false;
        }
        return true;
    }

    internal void CallbackPost( nint ptr )
    {
        var ctx = new DatamapFunctionHookContext<T>() {
            DatamapObject = Helper.AsSchema<T>(ptr),
            HookResult = HookResult.Continue
        };
        foreach (var callback in _PostCallbacks)
        {
            callback(ctx);
        }
    }

    public void HookPre( Action<IDatamapFunctionHookContext<T>> callback )
    {
        _Owner.Hook();
        _PreCallbacks.Add(callback);
    }

    public void HookPost( Action<IDatamapFunctionHookContext<T>> callback )
    {
        _Owner.Hook();
        _PostCallbacks.Add(callback);
    }

    public void Invoke( T schemaObject )
    {
        _Owner.Invoke(schemaObject.Address);
    }

    public void InvokeOriginal( T schemaObject )
    {
        _Owner.InvokeOriginal(schemaObject.Address);
    }

}