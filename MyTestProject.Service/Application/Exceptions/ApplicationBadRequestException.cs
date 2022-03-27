namespace MyTestProject.Service.Application.Exceptions;


public class ApplicationBadRequestException : ApplicationExceptionBase
{
    public ApplicationBadRequestException(string message = null) : base(400, message)
    {
    }
}