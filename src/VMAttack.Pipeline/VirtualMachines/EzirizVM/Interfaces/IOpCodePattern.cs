using AsmResolver.PE.DotNet.Cil;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Interfaces;

public interface IOpCodePattern : IPattern
{
    CilOpCode? CilOpCode => null;
    SpecialOpCodes? SpecialOpCode => null;

    /// <summary>
    ///     Whether the pattern can translate to CIL opcodes or is a special vm action.
    /// </summary>
    bool IsSpecial => false;

    /// <summary>
    ///     Whether the pattern can be matched more than once.
    /// </summary>
    bool AllowMultiple => false;

    /// <summary>
    ///     Additional verification to ensure the match is valid.
    /// </summary>
    /// <param name="handler">Handler the pattern is for</param>
    /// <returns>Whether verification is successful</returns>
    bool Verify(EzirizHandler handler) => Verify(handler.Instructions);
}