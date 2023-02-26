namespace VMAttack.Pipeline.VirtualMachines.EzirizVM.Architecture;

public class EzirizMethod
{
    public EzirizMethod(int methodKey)
    {
        MethodKey = methodKey;
        EzirizBody = new EzirizMethodBody(this);
    }

    public int MethodKey
    {
        get;
        set;
    }

    public EzirizMethodBody EzirizBody
    {
        get;
        set;
    }
}