namespace StargateAPI.Business.Exceptions;

public class BusinessRuleBrokenException : Exception
{
    public BusinessRuleBrokenException() { }

    public BusinessRuleBrokenException(string message) : base(message)
    { } 
}