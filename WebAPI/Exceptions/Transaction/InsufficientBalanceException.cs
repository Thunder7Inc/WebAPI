using System.Runtime.Serialization;

namespace WebAPI.Exceptions.Transaction;

[Serializable]
public class InsufficientBalanceException : Exception
{
    private string msg;
    public InsufficientBalanceException()
    {
        msg = "InSufficient Balance Exception";
    }

    public InsufficientBalanceException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}