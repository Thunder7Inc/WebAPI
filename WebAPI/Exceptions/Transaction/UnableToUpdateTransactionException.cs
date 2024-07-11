using System.Runtime.Serialization;

namespace WebAPI.Exceptions.Transaction;

[Serializable]
public class UnableToUpdateTransactionException : Exception
{
    private string msg;
    public UnableToUpdateTransactionException()
    {
        msg = "Unable to update a transaction";
    }

    public UnableToUpdateTransactionException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}