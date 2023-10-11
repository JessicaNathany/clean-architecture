using bookfy.domain.Abstractions;

namespace bookfy.domain.Bookings.Events
{
    public sealed record BookingReservedDomainEvent(Guid BoogkindId) : IDomainEvent;
}
