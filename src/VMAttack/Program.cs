using System.Diagnostics;
using Sharprompt;
using VMAttack.Core;
using VMAttack.Pipeline;

namespace VMAttack;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "VMAttack";
        Console.ForegroundColor = ConsoleColor.White;

        var logger = new ConsoleLogger();

        try
        {
            var userOptions = Prompt.Bind<Options>();
            var blackBoxAttack = new BlackBox(userOptions, logger);
            blackBoxAttack.Start();
            blackBoxAttack.Save();
        }
        catch (Exception ex) when (!Debugger.IsAttached)
        {
            logger.Error("An error occurred, maybe try newest version?");
            logger.Error(ex.ToString());
        }
        finally
        {
            logger.Info("Press any key to continue...");
            Console.ReadKey();
        }
    }
}