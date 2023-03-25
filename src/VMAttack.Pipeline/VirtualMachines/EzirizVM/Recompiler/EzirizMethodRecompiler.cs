using VMAttack.Core;
using VMAttack.Core.Abstraction;
using VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Recompiler;

public class EzirizMethodRecompiler : ContextBase
{
    public EzirizMethodRecompiler(Context context) : base(context, context.Logger)
    {

    }

    public void RecompileMethodBody(EzirizMethod vmMethod)
    {

    }
}