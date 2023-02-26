namespace VMAttack.Core.Interfaces;

/// <summary>
///     The <c>IVirtualMachine</c> interface defines the methods that must be implemented
///     by classes that represent a virtual machine that can be attacked by VMAttack.
/// </summary>
public interface IVirtualMachine
{
    /// <summary>
    ///     Gets the type of virtual machine represented by the implementing class.
    /// </summary>
    /// <value>The type of virtual machine represented by the implementing class.</value>
    VirtualMachineType Target
    {
        get;
    }

    /// <summary>
    ///     Executes the patch on the virtual machine.
    /// </summary>
    void Devirtualize();
}