using bookfy.domain.Abstractions;
using MediatR;

namespace bookfy.application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
