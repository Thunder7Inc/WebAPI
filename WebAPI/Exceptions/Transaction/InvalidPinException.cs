using System.Runtime.Serialization;

namespace WebAPI.Exceptions.Transaction;

[Serializable]
public class InvalidPinException : Exception
{
    private string msg;
    public InvalidPinException()
    {
        msg = "Invalid Pin";
    }

    public InvalidPinException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}