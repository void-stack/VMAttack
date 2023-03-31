using System;
using AsmResolver.DotNet;
using AsmResolver.IO;
using AsmResolver.PE.DotNet.Cil;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Recompiler;

//using VMAttack.Pipeline.VirtualMachines.EzirizVM.Recompiler;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM;

/// <summary>
///     This class provides an implementation of the <see cref="VirtualMachineAttackBase" /> class
///     for attacking the Eziriz .NET obfuscator.
/// </summary>
public class EzirizAttack : VirtualMachineAttackBase
{
    private readonly CilMethodBodyGenerator _bodyGenerator;
    private readonly EzirizDisassembler _ezirizDisassembler;
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

        _bodyGenerator = new CilMethodBodyGenerator(context, _streamReader);
    }

    /// <summary>
    ///     Gets the element of virtual machine this attack targets.
    /// </summary>
    public override VirtualMachineType Target => VirtualMachineType.Eziriz;

    /// <summary>
    ///     This method is called to perform the devirtualization attack.
    /// </summary>
    public override void Devirtualize()
    {
        foreach (var methodKey in _streamReader.MethodKeys)
        {
            var disassembledMethod = _ezirizDisassembler.GetOrCreateMethod(methodKey.Key, methodKey.Value);

            if (disassembledMethod.EzirizBody.FullyIdentified || Context.Options.WriteNotCompletedVirtualizedBodies)
            {
                var recompiledBody = _bodyGenerator.Compile(disassembledMethod.EzirizBody);
                disassembledMethod.PhysicalParent.CilMethodBody = recompiledBody;

                Logger.Info($"Method {disassembledMethod} is fully identified and recompiled to: {disassembledMethod.PhysicalParent.Name}");

                var formatter = new CilInstructionFormatter();
                foreach (var instruction in recompiledBody.Instructions)
                    Logger.Debug(formatter.FormatInstruction(instruction));

                disassembledMethod.EzirizBody.Decompiled = true;
            }
        }

        Logger.Info("Finished devirtualization!\n");
        foreach (var method in _streamReader.MethodKeys)
        {
            var decompiled = _ezirizDisassembler.GetOrCreateMethod(method.Key, method.Value);

            var parent = decompiled.PhysicalParent;
            var body = decompiled.EzirizBody;

            if (!body.FullyIdentified)
            {
                Logger.Info(
                    $"{parent.Name} (0x{parent.MetadataToken.ToInt32():X4})");
                Logger.Info($"\tDecompiled: {body.Decompiled} | Fully Identified: {body.FullyIdentified}.");
                Logger.Info($"\tIdentified Percentage: {decompiled.ResolvedPercentage:P2}.");
                Logger.Info(
                    $"\tIdentified Instructions: [{decompiled.ResolvedInstructions}/{body.Instructions.Count}] missing {decompiled.MissingInstructions} instructions.");
            }
            else
            {
                Logger.Info(
                    $"{parent.Name} (0x{parent.MetadataToken.ToInt32():X4}) is fully identified and recompiled.");
            }
        }
        Console.Write('\n');
    }

    /// <summary>
    ///     Determines whether the specified module matches the signature of an Eziriz virtualized module.
    /// </summary>
    /// <param name="module">The module to check for a match.</param>
    /// <returns><c>false</c> always because the EzirizAttack does not have a matching signature.</returns>
    public override bool MatchSignature(ModuleDefinition module) => false;
}