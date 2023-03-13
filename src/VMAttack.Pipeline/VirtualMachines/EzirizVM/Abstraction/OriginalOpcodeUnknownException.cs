using System;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;

public class OriginalOpcodeUnknownException : Exception
{
    public OriginalOpcodeUnknownException(EzirizOpcode instruction)
    {
        EzirizOpcode = instruction;
    }

    public EzirizOpcode EzirizOpcode { get; }
}