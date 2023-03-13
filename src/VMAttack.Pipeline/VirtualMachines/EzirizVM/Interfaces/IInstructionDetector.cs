using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

public interface IInstructionDetector
{
    CilCode Identify(EzirizOpcode instruction);
    bool TryIdentify(EzirizOpcode instruction, out CilCode code);
}