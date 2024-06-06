namespace Kildetoft.SimpleSQLite.Exceptions;

public class InvalidConnectionStringException : Exception
{
    public InvalidConnectionStringException(string message) : base(message) { }
    public InvalidConnectionStringException(string message, Exception innerException) : base(message, innerException) { }
}
