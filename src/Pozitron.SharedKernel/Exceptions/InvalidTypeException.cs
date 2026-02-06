namespace Pozitron.SharedKernel;

public class InvalidTypeException : AppException
{
    /// <summary>
    /// Initializes a new instance of the InvalidTypeException class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public InvalidTypeException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the InvalidTypeException class with a specified error message and an inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that triggered this exception.</param>
    public InvalidTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
