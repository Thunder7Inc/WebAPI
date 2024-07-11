using System.Runtime.Serialization;

namespace WebAPI.Exceptions.Transaction;

[Serializable]
public class NoSuchTransactionExistException : Exception
{
    private string msg;
    public NoSuchTransactionExistException()
    {
        msg = "No Such Transaction exists!";
    }

    public NoSuchTransactionExistException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}