using System.ComponentModel.DataAnnotations;

namespace VMAttack.Core;

/// <summary>
///     The <c>VirtualMachineType</c> enum represents the types of virtual machines that
///     can be attacked by VMAttack.
/// </summary>
public enum VirtualMachineType
{
    /// <summary>
    ///     Indicates that VMAttack should attack the Eziriz .NET Reactor protection.
    /// </summary>
    [Display(Name = "Eziriz .NET Reactor Protection.")]
    Eziriz,

    /// <summary>
    ///     Indicates that VMAttack should attack the Eazfuscator: .NET Obfuscator and Optimizer.
    /// </summary>
    [Display(Name = "Eazfuscator: .NET Obfuscator and Optimizer.")]
    Eazfuscator,

    /// <summary>
    ///     Indicates that VMAttack should attack the VMProtect Software Protection.
    /// </summary>
    [Display(Name = "VMProtect Software Protection.")]
    VmProtect,

    /// <summary>
    ///     Indicates that VMAttack should try to autodetect the virtual machine and attack it.
    /// </summary>
    [Display(Name = "Autodetection.")]
    Autodetect
}