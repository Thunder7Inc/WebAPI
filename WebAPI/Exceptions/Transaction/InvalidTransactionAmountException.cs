using System.Runtime.Serialization;

namespace WebAPI.Exceptions.Transaction;

[Serializable]
public class InvalidTransactionAmountException : Exception
{
    private string msg;
    public InvalidTransactionAmountException()
    {
        msg = "Invalid Transaction!";
    }

    public InvalidTransactionAmountException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}