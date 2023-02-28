using VMAttack.Core;
using VMAttack.Core.Interfaces;

namespace VMAttack.Pipeline;

/// <summary>
///     The BlackBox class provides a black box for executing attacks on a .NET module.
/// </summary>
public class BlackBox
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BlackBox" /> class.
    /// </summary>
    /// <param name="options">The options for the attack.</param>
    /// <param name="logger">The logger for the attack.</param>
    public BlackBox(Options options, ILogger logger)
    {
        // Create a new context with the given options and logger
        Context = new Context(options, logger);

        // Create a new virtual machine service with the context
        VirtualMachineService = new VirtualMachineService(Context);
    }

    /// <summary>
    ///     Gets the virtual machine service for this black box.
    /// </summary>
    private VirtualMachineService VirtualMachineService { get; }

    /// <summary>
    ///     Gets the context for this black box.
    /// </summary>
    private Context Context { get; }

    /// <summary>
    ///     Starts the attack on the .NET module.
    /// </summary>
    public void Start()
    {
        // Execute the virtual machine attack
        ExecuteVirtualMachinesAttack();
    }

    /// <summary>
    ///     Saves the patched .NET module.
    /// </summary>
    public void Save()
    {
        // Write the module to disk
        Context.WriteModule();
    }

    /// <summary>
    ///     Executes an attack on any virtual machines in the .NET module.
    /// </summary>
    private void ExecuteVirtualMachinesAttack()
    {
        // Use the virtual machine service to execute the virtual machine patch
        VirtualMachineService.ExecuteVirtualMachinePatch();
    }
}