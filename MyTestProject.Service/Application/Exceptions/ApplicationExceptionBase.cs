namespace MyTestProject.Service.Application.Exceptions;

public class ApplicationExceptionBase : Exception
{
    public int Code { get; set; }
    public ApplicationExceptionBase(int Code, string message) : base(message)
    {
        this.Code = Code;
    }
}