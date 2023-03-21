using AsmResolver.DotNet;
using AsmResolver.IO;
using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Disassembly;
//using VMAttack.Pipeline.VirtualMachines.EzirizVM.Recompiler;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM;

/// <summary>
///     This class provides an implementation of the <see cref="VirtualMachineAttackBase" /> class
///     for attacking the Eziriz .NET obfuscator.
/// </summary>
public class EzirizAttack : VirtualMachineAttackBase
{
    private readonly EzirizDisassembler _ezirizDisassembler;
    private readonly EzirizStreamReader _streamReader;
    //private readonly EzirizMethodRecompiler _recompiler;

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
        //_recompiler = new EzirizMethodRecompiler(context);
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
        foreach (var methodKey in _streamReader.MethodKeys)
        {
            var disassembledMethod = _ezirizDisassembler.GetOrCreateMethod(methodKey.Key, methodKey.Value);

            if (disassembledMethod.EzirizBody.FullyIdentified)
            {
                Logger.Info($"Method {disassembledMethod} is fully identified.");
                //_recompiler.RecompileMethodBody(disassembledMethod);
            }
        }
    }

    /// <summary>
    ///     Determines whether the specified module matches the signature of an Eziriz virtualized module.
    /// </summary>
    /// <param name="module">The module to check for a match.</param>
    /// <returns><c>false</c> always because the EzirizAttack does not have a matching signature.</returns>
    public override bool MatchSignature(ModuleDefinition module) => false;
}