using AsmResolver.DotNet;
using VMAttack.Core.Interfaces;

namespace VMAttack.Core.Abstraction;

/// <summary>
///     This abstract class represents an attack on a virtual machine in a .NET module.
/// </summary>
public abstract class VirtualMachineAttackBase : ContextBase, IVirtualMachine
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="VirtualMachineAttackBase" /> class.
    /// </summary>
    /// <param name="context">The context for the attack.</param>
    protected VirtualMachineAttackBase(Context context)
        : base(context, context.Logger) // Init base class
    {
    }

    /// <summary>
    ///     Gets the type of virtual machine this attack targets.
    /// </summary>
    public abstract VirtualMachineType Target
    {
        get;
    }

    /// <summary>
    ///     Executes the patch for this attack.
    /// </summary>
    public abstract void Devirtualize();

    /// <summary>
    ///     Determines whether the given module matches the signature of the virtual machine
    ///     targeted by this attack.
    /// </summary>
    /// <param name="module">The module to check.</param>
    /// <returns>
    ///     true if the module matches the signature of the targeted virtual machine;
    ///     otherwise, false.
    /// </returns>
    public abstract bool MatchSignature(ModuleDefinition module);
}