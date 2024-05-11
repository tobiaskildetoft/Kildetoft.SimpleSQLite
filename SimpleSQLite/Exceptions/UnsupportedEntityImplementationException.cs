namespace Kildetoft.SimpleSQLite.Exceptions;

public class UnsupportedEntityImplementationException : Exception
{
    public UnsupportedEntityImplementationException(string message) : base(message) { }
    public UnsupportedEntityImplementationException(string message, Exception innerException) : base(message, innerException) { }
}
