namespace WebAPI.Exceptions.Account;

using System.Runtime.Serialization;
[Serializable]
public class UnableToUpdateAccountException : Exception
{
    private string msg;
    public UnableToUpdateAccountException()
    {
        msg = "Unable to update a account!";
    }

    public UnableToUpdateAccountException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}