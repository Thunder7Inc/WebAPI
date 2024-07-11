namespace WebAPI.Exceptions.Account;

using System.Runtime.Serialization;
[Serializable]
public class NoSuchAccountExistException : Exception
{
    private string msg;
    public NoSuchAccountExistException()
    {
        msg = "No Such Account Exists!";
    }

    public NoSuchAccountExistException(string message) : base(message)
    {
        msg = message;
    }

    public override string Message => msg;
}