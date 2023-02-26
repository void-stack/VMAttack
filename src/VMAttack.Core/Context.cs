using System.IO;
using AsmResolver.DotNet;
using AsmResolver.DotNet.Builder;
using AsmResolver.PE;
using AsmResolver.PE.DotNet.Builder;
using AsmResolver.PE.File;
using VMAttack.Core.Interfaces;

namespace VMAttack.Core;

/// <summary>
///     Class Context provides a runtime context for the VMAttack application.
/// </summary>
public class Context
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Context" /> class with the specified
    ///     options and logger.
    /// </summary>
    /// <param name="options">The options to use when creating the context.</param>
    /// <param name="logger">The logger to use when creating the context.</param>
    public Context(Options options, ILogger logger)
    {
        Logger = logger;
        Options = options;

        if (!File.Exists(options.InputFile))
            throw new FileNotFoundException($"Input file {options.InputFile} not found.");

        logger.Debug($"Loading PEFile ({options.InputFile})");
        PeFile = PEFile.FromFile(options.InputFile);

        logger.Debug("Loading PEImage...");
        var peImage = PEImage.FromFile(PeFile);

        logger.Debug("Loading ModuleDefinition...");
        Module = ModuleDefinition.FromImage(peImage);
    }

    /// <summary>
    ///     Gets the PE file that this context operates on.
    /// </summary>
    /// <value>The PE file that this context operates on.</value>
    public PEFile PeFile
    {
        get;
    }

    /// <summary>
    ///     Gets the logger that this context uses for logging.
    /// </summary>
    /// <value>The logger that this context uses for logging.</value>
    public ILogger Logger
    {
        get;
    }

    /// <summary>
    ///     Gets the options that this context uses.
    /// </summary>
    /// <value>The options that this context uses.</value>
    public Options Options
    {
        get;
    }

    /// <summary>
    ///     Gets the module definition for the module that this context operates on.
    // </<summary>
    public ModuleDefinition Module
    {
        get;
    }

    /// <summary>
    ///     Writes the modified module to disk. The file will be saved to the specified output path.
    ///     If the output path does not exist, it will be created.
    /// </summary>
    public void WriteModule()
    {
        // Get the directory where the input file is located
        string? targetDirectory = Path.GetDirectoryName(Options.InputFile);

        // Get the name of the input file without the file extension
        string? fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Options.InputFile);

        // Create the directory where the modified file will be saved
        string directory = Path.Combine(targetDirectory!, $"{fileNameWithoutExtension}-modified");

        // Create the full path of the modified file
        string newFilename = Path.Combine(directory, $"{fileNameWithoutExtension}.patched.exe");

        // If the directory does not exist, create it
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Create an image builder
        var imageBuilder = new ManagedPEImageBuilder
        {
            DotNetDirectoryFactory = new DotNetDirectoryFactory(MetadataBuilderFlags.PreserveAll)
        };

        // Use the image builder to create an image from the module
        var result = imageBuilder.CreateImage(Module);

        // Create a file builder
        var fileBuilder = new ManagedPEFileBuilder();

        // Use the file builder to create a file from the image
        var file = fileBuilder.CreateFile(result.ConstructedImage!);

        // Write the file to disk
        file.Write(newFilename);

        // Check if the file was written successfully
        if (File.Exists(newFilename))
            // Log a success message
            Logger.Info($"File written to {newFilename}");
        else
            // Log an error message
            Logger.Error($"Failed to write file to {newFilename}");
    }
}