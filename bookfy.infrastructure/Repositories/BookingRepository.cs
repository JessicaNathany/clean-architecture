using bookfy.domain.Apartaments;
using bookfy.domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace bookfy.infrastructure.Repositories
{
    internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private static readonly BookingStatus[] ActiveBookingStatus =
        {
            BookingStatus.Reserved,
            BookingStatus.Confirmed,
            BookingStatus.Completed,
        };

        public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsOverlappingAsync(Apartament apartament, DateRange duration, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<Booking>()
                .AnyAsync(booking =>
                booking.ApartamentId == apartament.Id &&
                booking.Duration.Start <= booking.Duration.End &&
                booking.Duration.End >= booking.Duration.Start &&
                ActiveBookingStatus.Contains(booking.Status), cancellationToken);
        }

        Task IBookingRepository.Add(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
