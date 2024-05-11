namespace Kildetoft.SimpleSQLite.Exceptions;

public class UnsupportedIndexImplementationException : Exception
{
    public UnsupportedIndexImplementationException(string message) : base(message) { }
    public UnsupportedIndexImplementationException(string message, Exception innerException) : base(message, innerException) { }
}
