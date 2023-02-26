using Serilog;
using Serilog.Core;
using ILogger = VMAttack.Core.Interfaces.ILogger;

namespace VMAttack;

public class ConsoleLogger : ILogger
{
    private readonly Logger _logger = new LoggerConfiguration().WriteTo.Console().MinimumLevel.Verbose().CreateLogger();

    public void Debug(string m)
    {
        _logger.Debug(m);
    }

    public void Error(string m)
    {
        _logger.Error(m);
    }

    public void Info(string m)
    {
        _logger.Information(m);
    }

    public void Warn(string m)
    {
        _logger.Warning(m);
    }

    public void Print(string m)
    {
        _logger.Verbose(m);
    }
}