namespace WebAPI.Exceptions.Account;

using System.Runtime.Serialization;
[Serializable]
public class UnableToDeleteAccountException : Exception
{
    private string msg;
    public UnableToDeleteAccountException()
    {
        msg = "Unable to delete a account!";
    }

    public UnableToDeleteAccountException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}