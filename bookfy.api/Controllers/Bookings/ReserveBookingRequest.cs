namespace bookfy.api.Controllers.Bookings
{
    public record ReserveBookingRequest(Guid ApartmentId, Guid UserId, DateOnly StartDate, DateOnly EndDate);
}
