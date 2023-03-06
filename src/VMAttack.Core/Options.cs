using System.ComponentModel.DataAnnotations;

namespace VMAttack.Core;

/// <summary>
///     The Options class represents the options for an attack on a .NET module.
/// </summary>
public class Options
{
    /// <summary>
    ///     Gets or sets the path to the input file.
    /// </summary>
    /// <value>The path to the input file.</value>
    [Required]
    [Display(Name = "Path to the file", Description = "Path to the file")]
    [MinLength(1)]
    [MaxLength(255)]
    public string InputFile { get; set; } =
        @"C:\Users\User\Desktop\VMAttack\src\Target_EzirizVM\bin\Debug\Target_EzirizVM_Secure\Target_EzirizVM_de4dot_Renamed.exe"; //null!;

    /// <summary>
    ///     Gets or sets the path to the output file.
    /// </summary>
    /// <value>The path to the output file.</value>
    [Required]
    [Display(Name = "Name of the patched binary", Description = "The patched binary will be saved to this path")]
    [MinLength(1)]
    [MaxLength(255)]
    public string OutputPath { get; set; } = "test.exe"; //null!;

    /// <summary>
    ///     Gets or sets the type of virtual machine to attack.
    /// </summary>
    /// <value>The type of virtual machine to attack.</value>
    [Required]
    [Display(Name = "Select your Virtual Machine Type", Description = "Select your Virtual Machine Type")]
    public VirtualMachineType VirtualMachine { get; set; }
}