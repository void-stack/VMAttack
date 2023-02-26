using System;
using System.Collections.Generic;
using System.Linq;
using VMAttack.Core.Abstraction;

namespace VMAttack.Core;

/// <summary>
///     This class provides a service for executing patches on virtual machines in a .NET module.
/// </summary>
public class VirtualMachineService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="VirtualMachineService" /> class.
    /// </summary>
    /// <param name="context">The context for the service.</param>
    public VirtualMachineService(Context context)
    {
        Context = context;
    }

    /// <summary>
    ///     Gets the context for this service.
    /// </summary>
    private Context Context
    {
        get;
    }

    /// <summary>
    ///     Gets a collection of types that implement the specified interface.
    /// </summary>
    /// <typeparam name="TInterface">The interface to search for implementations of.</typeparam>
    /// <returns>A collection of types that implement <typeparamref name="TInterface" />.</returns>
    private static IEnumerable<Type> GetImplementationsOf<TInterface>()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(TInterface)) && !t.IsAbstract)
            .ToList();
    }

    /// <summary>
    ///     Gets a collection of virtual machine attacks that have been implemented.
    /// </summary>
    /// <returns>
    ///     A collection of <see cref="VirtualMachineAttackBase" /> objects representing
    ///     the implemented virtual machine attacks.
    /// </returns>
    private IEnumerable<VirtualMachineAttackBase> GetImplementedVirtualMachines()
    {
        return GetImplementationsOf<VirtualMachineAttackBase>()
            .Select(type => (VirtualMachineAttackBase)Activator.CreateInstance(type, Context))
            .ToList();
    }

    /// <summary>
    ///     Executes patches on virtual machines in the specified .NET module.
    /// </summary>
    public void ExecuteVirtualMachinePatch()
    {
        // If the virtual machine to be patched is set to Autodetect, check if the
        // module matches the signature of the virtual machine before executing the patch.
        foreach (var virtualMachine in GetImplementedVirtualMachines())
        {
            // If the virtual machine to be patched is NOT set to Autodetect, execute the patch.
            if (virtualMachine.Target != VirtualMachineType.Autodetect)
            {
                virtualMachine.Devirtualize();
                return;
            }

            // If the virtual machine to be patched is set to Autodetect, check if the
            // module matches the signature of the virtual machine before executing the patch.
            if (!virtualMachine.MatchSignature(Context.Module))
                continue;

            Context.Options.VirtualMachine = virtualMachine.Target;
            virtualMachine.Devirtualize();
        }
    }
}