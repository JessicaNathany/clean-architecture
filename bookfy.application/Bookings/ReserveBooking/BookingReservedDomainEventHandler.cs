using bookfy.application.Abstractions.Email;
using bookfy.domain.Bookings;
using bookfy.domain.Bookings.Events;
using bookfy.domain.Users;
using MediatR;

namespace bookfy.application.Bookings.ReserveBooking
{
    internal sealed class BookingReservedDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
    {
        private readonly IBookingRepository _bookingREpository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public BookingReservedDomainEventHandler(
            IBookingRepository bookingREpository, 
            IUserRepository userRepository, 
            IEmailService emailService)
        {
            _bookingREpository = bookingREpository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
        {
            var booking = await _bookingREpository.GetByIdAsync(notification.BoogkindId, cancellationToken);

            if (booking is null)
                return;

            var user = await _userRepository.GetByIdAsync(booking.UserId, cancellationToken);

            if (user is null)
                return;

            await _emailService.SendAsync(user.Email, "Booking reserved!", "You have 10 minutes to confirm this bookin");
        }
    }
}
