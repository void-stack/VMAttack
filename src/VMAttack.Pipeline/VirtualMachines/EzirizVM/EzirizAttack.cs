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
    private readonly CustomDataReader _dataReader;

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
        _dataReader = new CustomDataReader(new BinaryStreamReader(), context);

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
        // Logs a message to indicate that the EzirizVM devirtualization attack is being performed.
        Logger.Info("EzirizVM");
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