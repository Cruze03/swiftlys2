using System.Runtime.InteropServices;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Datamaps;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Datamaps;

internal class BaseDatamapFunction<T>( string functionName ) : IDatamapFunction<T>
    where T : ISchemaClass<T>
{
    public string FunctionName { get; } = functionName;
    private bool _Initialized { get; set; }
    private bool _Hooked { get; set; }
    private nint _OriginalFunctionPtr { get; set; }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void StubDelegate( nint a1 );
    private StubDelegate? _keptAliveDelegate;

    private readonly Dictionary<string, BaseDatamapFunctionOperator<T>> _Operators = [];

    public IDatamapFunctionOperator<T> Get( string pluginId )
    {
        if (!_Operators.TryGetValue(pluginId, out var op))
        {
            op = new BaseDatamapFunctionOperator<T>(this);
            _Operators.Add(pluginId, op);
        }
        return op;
    }

    private nint GetDatamapFunctionAddressPtr()
    {
        // TODO
        return 0;
    }

    private void Stub( nint a1 )
    {
        foreach (var op in _Operators.Values)
        {
            if (!op.CallbackPre(a1)) return;
        }
        InvokeOriginal(a1);
        foreach (var op in _Operators.Values)
        {
            op.CallbackPost(a1);
        }
    }

    internal void Initialize()
    {
        if (_Initialized) return;
        _OriginalFunctionPtr = GetDatamapFunctionAddressPtr().Read<nint>();
        if (_OriginalFunctionPtr == nint.Zero)
        {
            throw new Exception($"Failed to get the address of the function {FunctionName}");
        }
        _Initialized = true;
    }

    internal void Invoke( nint a1 )
    {
        Stub(a1);
    }

    internal void InvokeOriginal( nint a1 )
    {
        unsafe
        {
            ((delegate* unmanaged< nint, void >)_OriginalFunctionPtr)(a1);
        }
    }

    internal void Hook()
    {
        if (_Hooked) return;
        Initialize();
        _keptAliveDelegate = new StubDelegate(Stub);
        var stubPtr = Marshal.GetFunctionPointerForDelegate(_keptAliveDelegate);
        GetDatamapFunctionAddressPtr().Write(stubPtr);
        _Hooked = true;
    }


}