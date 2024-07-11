using System.Runtime.Serialization;

namespace WebAPI.Exceptions.Transaction;

[Serializable]
public class TransactionLimitExceededException : Exception
{
    private string msg;
    public TransactionLimitExceededException()
    {
        msg = "Not Enough Balance in your account!";
    }

    public TransactionLimitExceededException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}