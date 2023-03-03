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
    private readonly Disassembler _disassembler;
    private readonly EzirizStreamReader _streamReader;

    /// <summary>
    ///     Gets the module explorer for this attack.
    /// </summary>
    private ModuleExplorer _moduleExplorer;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EzirizAttack" /> class.
    /// </summary>
    /// <param name="context">The context for the attack.</param>
    public EzirizAttack(Context context)
        : base(context)
    {
        // Initializes a new instance of the CustomDataReader with a BinaryStreamReader and the provided context.
        _streamReader = new EzirizStreamReader(context, new BinaryStreamReader());
        _disassembler = new Disassembler(context, _streamReader);

        // Initializes a new instance of the ModuleExplorer with the context.Module.
        _moduleExplorer = new ModuleExplorer(context.Module);
    }

    /// <summary>
    ///     Gets the type of virtual machine this attack targets.
    /// </summary>
    public override VirtualMachineType Target => VirtualMachineType.Eziriz;

    /// <summary>
    ///     This method is called to perform the devirtualization attack.
    /// </summary>
    public override void Devirtualize()
    {
        Console.Write("\n");
        foreach (var methodKey in _streamReader.MethodKeys)
        {
            _disassembler.GetOrCreateMethod(methodKey.Key, methodKey.Value);
            Console.Write("\n");
        }

        // Get all distinct opcodes used in virtualized method sorted  
        Logger.Debug("Unique used opcodes used in disassembler: byte[] {" +
                     string.Join(", ", _disassembler.UsedOpcodesMap) + "};");

        // Pattern match the unique opcodes? make ast?
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