using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CUtlMemoryFixedGrowable<T, TBuffer>
    where T : unmanaged
    where TBuffer : unmanaged, IFixedBuffer
{
    private nint _memory;
    private int _allocationCount;
    private int _growSize;
    private TBuffer _fixedMemory;

    public CUtlMemoryFixedGrowable(int size, int growSize = 0)
    {
        _allocationCount = size;
        _growSize = growSize | unchecked((int)0x80000000);

        _memory = (nint)Unsafe.AsPointer(ref _fixedMemory);
    }

    public readonly nint Base => _memory;
    public readonly int AllocationCount => _allocationCount;
}

public interface IFixedBuffer
{
}

[InlineArray(512)]
public struct FixedCharBuffer512 : IFixedBuffer
{
    private byte _element0;
}

[InlineArray(64)]
public struct FixedPtrBuffer64 : IFixedBuffer
{
    private nint _element0;
}