using System;
using AsmResolver.DotNet;
using AsmResolver.IO;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM;

/// <summary>
///     This class provides an implementation of the <see cref="VirtualMachineAttackBase" /> class
///     for attacking the Eziriz .NET obfuscator.
/// </summary>
public class EzirizAttack : VirtualMachineAttackBase
{
    private readonly EzirizDisassembler _ezirizDisassembler;

    /// <summary>
    ///     Gets the module explorer for this attack.
    /// </summary>
    private readonly ModuleExplorer _moduleExplorer;

    private readonly EzirizStreamReader _streamReader;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EzirizAttack" /> class.
    /// </summary>
    /// <param name="context">The context for the attack.</param>
    public EzirizAttack(Context context)
        : base(context)
    {
        // Initializes a new instance of the CustomDataReader with a BinaryStreamReader and the provided context.
        _streamReader = new EzirizStreamReader(context, new BinaryStreamReader());
        _ezirizDisassembler = new EzirizDisassembler(context, _streamReader);

        // Initializes a new instance of the ModuleExplorer with the context.Module.
        _moduleExplorer = new ModuleExplorer(context.Module);
    }

    /// <summary>
    ///     Gets the type of virtual machine this attack targets.
    /// </summary>
    public override VirtualMachineType Target
    {
        get { return VirtualMachineType.Eziriz; }
    }

    /// <summary>
    ///     This method is called to perform the devirtualization attack.
    /// </summary>
    public override void Devirtualize()
    {
        Console.Write("\n");
        foreach (var methodKey in _streamReader.MethodKeys)
        {
            var disassembledMethod = _ezirizDisassembler.GetOrCreateMethod(methodKey.Key, methodKey.Value);
            int resolved = 0;
            
            foreach (var instruction in disassembledMethod.EzirizBody.Instructions)
            {
                var opcode = instruction.Opcode;

                // TODO: This is just a test, remove this later.
                bool success = opcode.TryIdentify(out var cilCode);
                resolved += success ? 1 : 0;
                
                Logger.Debug(success
                    ? $"\tVM_{instruction.Offset:X4}: opcode_{cilCode}" +
                      (instruction.Operand != null ? $" : {instruction.Operand}" : string.Empty)
                    : $"\tVM_{instruction.Offset:X4}: {opcode}" +
                      (instruction.Operand != null ? $" : {instruction.Operand}" : string.Empty));
            }

            // calculate the percentage of resolved instructions.
            double percentage = (double) resolved / disassembledMethod.EzirizBody.Instructions.Count * 100;
            Logger.Info($"Resolved {resolved} instructions out of {disassembledMethod.EzirizBody.Instructions.Count} ({percentage:F2}%)");
            
            Console.Write("\n");
        }
    }

    /// <summary>
    ///     Determines whether the specified module matches the signature of an Eziriz virtualized module.
    /// </summary>
    /// <param name="module">The module to check for a match.</param>
    /// <returns><c>false</c> always because the EzirizAttack does not have a matching signature.</returns>
    public override bool MatchSignature(ModuleDefinition module)
    {
        return false;
    }
}