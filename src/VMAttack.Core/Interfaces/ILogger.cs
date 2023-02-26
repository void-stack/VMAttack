namespace VMAttack.Core.Interfaces;

public interface ILogger
{
    public void Debug(string m);
    public void Error(string m);
    public void Info(string m);
    public void Warn(string m);
    public void Print(string m);
}