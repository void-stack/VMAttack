using VMAttack.Core.Interfaces;

namespace VMAttack.Core.Abstraction;

public abstract class ContextBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ContextBase" /> class.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    protected ContextBase(Context context, ILogger logger)
    {
        Context = context;
        Logger = logger;
    }

    /// <summary>
    ///     Gets the logger for this attack.
    /// </summary>
    protected ILogger Logger
    {
        get;
    }

    /// <summary>
    ///     Gets the context for this attack.
    /// </summary>
    protected Context Context
    {
        get;
    }
}