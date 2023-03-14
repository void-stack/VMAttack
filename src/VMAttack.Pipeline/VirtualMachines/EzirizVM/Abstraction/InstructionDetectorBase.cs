using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Abstraction;

public abstract class InstructionDetectorBase : IInstructionDetector
{
    public abstract CilCode Identify(EzirizOpcode instruction);

    public bool TryIdentify(EzirizOpcode instruction, out CilCode code)
    {
        try
        {
            code = Identify(instruction);
            return true;
        }
        catch (OriginalOpcodeUnknownException)
        {
            code = CilCode.Nop;
            return false;
        }
    }
}