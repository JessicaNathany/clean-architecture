using bookfy.application.Abstractions.Messaging;

namespace bookfy.application.Bookings.GetBookings
{
    public sealed record GetBookingQuery(Guid BookingID) : IQuery<BookingResponse>;
}
