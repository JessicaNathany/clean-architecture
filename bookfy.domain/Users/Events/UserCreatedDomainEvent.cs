using bookfy.domain.Abstractions;

namespace bookfy.domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(Guid userId) : IDomainEvent;
}

