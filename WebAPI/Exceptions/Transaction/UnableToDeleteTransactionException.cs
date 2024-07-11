using System.Runtime.Serialization;

namespace WebAPI.Exceptions.Transaction;

[Serializable]
public class UnableToDeleteTransactionException : Exception
{
    private string msg;
    public UnableToDeleteTransactionException()
    {
        msg = "Unable to delete a transaction";
    }

    public UnableToDeleteTransactionException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}