

namespace WebAPI.Exceptions.Account;

using System.Runtime.Serialization;
[Serializable]
public class UnableToAddAccountException : Exception
{
    private string msg;
    public UnableToAddAccountException()
    {
        msg = "Unable to add a account!";
    }

    public UnableToAddAccountException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}