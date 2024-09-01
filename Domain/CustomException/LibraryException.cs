namespace Domain.CustomException;

public class LibraryException : Exception
{
    public LibraryException()
    {
        
    }

    public LibraryException(string message) : base(message)
    {
        
    }

    public LibraryException(string message , Exception innerException) : base(message,innerException)
    {
        
    }
}