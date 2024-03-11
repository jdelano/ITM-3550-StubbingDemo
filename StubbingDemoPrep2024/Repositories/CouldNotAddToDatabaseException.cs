namespace StubbingDemoPrep2024;

public class CouldNotAddToDatabaseException : Exception
{
    public CouldNotAddToDatabaseException() : base("A Database error occurred.") { }
    public CouldNotAddToDatabaseException(string message) : base(message) { }
    public CouldNotAddToDatabaseException(string message, Exception exception)
        : base(message, exception) { }
}

