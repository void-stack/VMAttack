using System;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

[Flags]
public enum EzirizType : byte
{
    Object = 0,
    SByte = 1,
    Byte = 2,
    Int16 = 3,
    UInt16 = 4,
    Int32 = 5,
    UInt32 = 6,
    Int64 = 7,
    UInt64 = 8,
    Single = 9,
    Double = 10,
    Boolean = 11,
    IntPtr = 12,
    UIntPtr = 13,
    String = 14,
    Char = 15
}

public class EzirizLocal
{
    public EzirizLocal(uint index, EzirizType type, bool isByRef)
    {
        Index = index;
        Type = type;
        IsByRef = isByRef;
    }

    public uint Index { get; }
    public bool IsByRef { get; }
    public EzirizType Type { get; }

    public override string ToString()
    {
        return $"V_{Index:X4} : Type {Type} : IsByRef: {IsByRef}";
    }
}