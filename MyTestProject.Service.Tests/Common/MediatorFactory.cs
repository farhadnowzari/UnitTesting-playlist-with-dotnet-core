using MediatR;
using Moq;

namespace MyTestProject.Service.Tests.Common;


public class MediatorFactory
{
    public static Mock<IMediator> GetMediatorMock()
    {
        return new Mock<IMediator>();
    }
}