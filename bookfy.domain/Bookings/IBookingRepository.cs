using bookfy.domain.Apartaments;

namespace bookfy.domain.Bookings
{
    public interface IBookingRepository
    {
        Task Add(Booking booking);
        Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<bool> IsOverlappingAsync(Apartament apartament, DateRange duration, CancellationToken cancellationToken);
    }
}
