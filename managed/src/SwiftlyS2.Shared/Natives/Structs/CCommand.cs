using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CCommand
{
    private enum COMMAND : int
    {
        MAX_ARGC = 64,
        MAX_LENGTH = 512,
    };

    private int _argv0Size;
    private CUtlVectorFixedGrowable<byte, FixedCharBuffer512> _argSBuffer;
    private CUtlVectorFixedGrowable<byte, FixedCharBuffer512> _argvBuffer;
    private CUtlVectorFixedGrowable<nint, FixedPtrBuffer64> _args;

    public CCommand()
    {
        _argv0Size = 0;
        _argSBuffer = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>((int)COMMAND.MAX_LENGTH);
        _argvBuffer = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>((int)COMMAND.MAX_LENGTH);
        _args = new CUtlVectorFixedGrowable<nint, FixedPtrBuffer64>((int)COMMAND.MAX_ARGC);
        EnsureBuffers();
        Reset();
    }

    public CCommand(string[] args)
    {
        _argv0Size = 0;
        _argSBuffer = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>((int)COMMAND.MAX_LENGTH);
        _argvBuffer = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>((int)COMMAND.MAX_LENGTH);
        _args = new CUtlVectorFixedGrowable<nint, FixedPtrBuffer64>((int)COMMAND.MAX_ARGC);
        EnsureBuffers();
        Reset();

        if (args == null || args.Length == 0)
        {
            return;
        }

        byte* pBuf = (byte*)_argvBuffer.Base;
        byte* pSBuf = (byte*)_argSBuffer.Base;

        for (int i = 0; i < args.Length; ++i)
        {
            _args.AddToTail((nint)pBuf);

            var argBytes = System.Text.Encoding.UTF8.GetBytes(args[i]);
            int nLen = argBytes.Length;

            fixed (byte* pArg = argBytes)
            {
                Unsafe.CopyBlock(pBuf, pArg, (uint)nLen);
            }
            pBuf[nLen] = 0;

            if (i == 0)
            {
                _argv0Size = nLen;
            }
            pBuf += nLen + 1;

            bool bContainsSpace = args[i].Contains(' ');
            if (bContainsSpace)
            {
                *pSBuf++ = (byte)'"';
            }

            fixed (byte* pArg = argBytes)
            {
                Unsafe.CopyBlock(pSBuf, pArg, (uint)nLen);
            }
            pSBuf += nLen;

            if (bContainsSpace)
            {
                *pSBuf++ = (byte)'"';
            }

            if (i != args.Length - 1)
            {
                *pSBuf++ = (byte)' ';
            }
        }
    }

    private void EnsureBuffers()
    {
        _argSBuffer.SetSize(MaxCommandLength());
        _argvBuffer.SetSize(MaxCommandLength());
    }

    public void Reset()
    {
        _argv0Size = 0;
        ((byte*)_argSBuffer.Base)[0] = 0;
        _args.RemoveAll();
    }

    public readonly int ArgC() => _args.Count;

    public readonly string? ArgS() => _argv0Size == 0 ? null : Marshal.PtrToStringUTF8(_argSBuffer.Base + _argv0Size);

    public readonly string? GetCommandString() => ArgC() == 0 ? null : Marshal.PtrToStringUTF8(_argSBuffer.Base);

    public readonly string? Arg(int index) => (index < 0 || index >= ArgC()) ? null : Marshal.PtrToStringUTF8((nint)_args[index]);

    public readonly string? this[int index] => Arg(index);

    public readonly int FindArg(string name)
    {
        int nArgC = ArgC();
        for (int i = 1; i < nArgC; i++)
        {
            var arg = Arg(i);
            if (arg != null && string.Equals(arg, name, StringComparison.OrdinalIgnoreCase))
            {
                return (i + 1) < nArgC ? i + 1 : -1;
            }
        }
        return -1;
    }

    public readonly int FindArgInt(string name, int defaultVal)
    {
        int idx = FindArg(name);
        if (idx != -1)
        {
            var arg = Arg(idx);
            if (arg != null && int.TryParse(arg, out int result))
            {
                return result;
            }
        }
        return defaultVal;
    }

    public static int MaxCommandLength() => (int)COMMAND.MAX_LENGTH - 1;
}