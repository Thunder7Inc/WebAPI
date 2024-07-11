

using System.Runtime.Serialization;

namespace WebAPI.Exceptions.Transaction;

[Serializable]
public class UnableToAddTransactionException : Exception
{
    private string msg;
    public UnableToAddTransactionException()
    {
        msg = "Unable to add a transaction";
    }

    public UnableToAddTransactionException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}